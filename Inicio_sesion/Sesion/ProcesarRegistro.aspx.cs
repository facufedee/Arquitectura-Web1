using System;
using System.Data.SqlClient;
using System.Web.UI;

public partial class ProcesarRegistro : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RegistrarUsuario();
        }
    }

    private void RegistrarUsuario()
    {
        string username = Request.Form["username"];
        string email = Request.Form["email"];
        string password = Request.Form["password"];
        string confirmPassword = Request.Form["confirmPassword"];

        if (password != confirmPassword)
        {
            Response.Write("Las contraseñas no coinciden.");
            return;
        }

        try
        {
            if (UsuarioExistente(username))
            {
                Response.Write("El nombre de usuario ya está en uso.");
                return;
            }

            // Aquí se inserta el nuevo usuario en la base de datos
            InsertarUsuario(username, email, password);

            Response.Write("Usuario registrado exitosamente.");
        }
        catch (Exception ex)
        {
            Response.Write("Ocurrió un error al registrar el usuario.");
            LogException(ex);
        }
    }

    private bool UsuarioExistente(string username)
    {
        string connectionString = "Data Source=Facanda\\SQLEXPRESS;Initial Catalog=TallerOesteWeb;Integrated Security=True;";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", username);

            conn.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar());

            return count > 0;
        }
    }

    private void InsertarUsuario(string username, string email, string password)
    {
        string connectionString = "Data Source=Facanda\\SQLEXPRESS;Initial Catalog=TallerOesteWeb;Integrated Security=True;";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Usuarios (NombreUsuario, CorreoElectronico, Contraseña) VALUES (@Username, @Email, @Password)", conn);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    private void LogException(Exception ex)
    {
        // Aquí implementa el código para registrar la excepción en un sistema de registro
    }
}
