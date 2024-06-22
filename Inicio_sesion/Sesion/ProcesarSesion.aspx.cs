using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.UI;

public partial class Validate : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ValidarUsuario();
        }
    }
   

    private void ValidarUsuario()
    {
        string username = Request.Form["username"];
        string password = Request.Form["password"];

        try
        {
            if (UsuarioCorrecto(username, password))
            {
                ResetearIntentos(username);

                Session["username"] = username;
                DateTime fechaHora = DateTime.Now;
                string evento = "Inicio de sesión exitoso"; // Define el evento a registrar
                RegistroEventos(username, evento, fechaHora); // Aquí cambia la llamada

                Response.Redirect("Home.aspx");  // Redirigir a la página de inicio o dashboard
            }
            else
            {
                IntentoFallido(username);
                DateTime fechaHora = DateTime.Now;
                string evento = "Inicio de sesión fallido"; // Define el evento a registrar
                RegistroEventos(username, evento, fechaHora); // Aquí cambia la llamada

            }
        }
        catch (Exception ex)
        {
            LogException(ex);
            lblMessage.Text = "Ocurrió un error. Por favor, intenta nuevamente más tarde.";
        }
    }

    private bool UsuarioCorrecto(string username, string password)
    {
        string connectionString = "Data Source=Facanda\\SQLEXPRESS;Initial Catalog=TallerOesteWeb;Integrated Security=True;";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("SP_VALIDAR_USUARIO", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);

            conn.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar());

            return count == 1;
        }
    }

    private void ResetearIntentos(string username)
    {
        string connectionString = "Data Source=Facanda\\SQLEXPRESS;Initial Catalog=TallerOesteWeb;Integrated Security=True;";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmdResetAttempts = new SqlCommand("SP_RESETEAR_INTENTOS", conn); // Nombre del stored procedure
            cmdResetAttempts.CommandType = System.Data.CommandType.StoredProcedure; // Especifica que es un stored procedure

            // Agregar parámetros al stored procedure si es necesario
            cmdResetAttempts.Parameters.AddWithValue("@Username", username);

            conn.Open();
            cmdResetAttempts.ExecuteNonQuery();
        }

    }

 
    private void RegistroEventos(string username, string evento, DateTime fechaHora)
    {
        string connectionString = "Data Source=Facanda\\SQLEXPRESS;Initial Catalog=TallerOesteWeb;Integrated Security=True;";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("SP_REGISTRO_BITACORA", conn); // Nombre del stored procedure
            cmd.CommandType = System.Data.CommandType.StoredProcedure; // Especifica que es un stored procedure

            // Agregar parámetros al stored procedure
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Evento", evento);
            cmd.Parameters.AddWithValue("@FechaHora", fechaHora);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    private void IntentoFallido(string username)
    {
        string connectionString = "Data Source=Facanda\\SQLEXPRESS;Initial Catalog=TallerOesteWeb;Integrated Security=True;";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmdBlocked = new SqlCommand("SELECT Estado FROM Usuario WHERE username = @Username", conn);
            cmdBlocked.Parameters.AddWithValue("@Username", username);

            conn.Open();
            int estado = Convert.ToInt32(cmdBlocked.ExecuteScalar());

            if (estado == 0)
            {
                lblMessage.Text = "El usuario está bloqueado. Por favor, contáctate con el administrador.";
            }
            else
            {
                SqlCommand cmdUpdateAttempts = new SqlCommand("UPDATE Usuario SET IntentosFallidos = IntentosFallidos + 1 WHERE username = @Username", conn);
                cmdUpdateAttempts.Parameters.AddWithValue("@Username", username);
                cmdUpdateAttempts.ExecuteNonQuery();

                SqlCommand cmdAttempts = new SqlCommand("SELECT IntentosFallidos FROM Usuario WHERE username = @Username", conn);
                cmdAttempts.Parameters.AddWithValue("@Username", username);
                int intentosFallidos = Convert.ToInt32(cmdAttempts.ExecuteScalar());

                if (intentosFallidos >= 3)
                {
                    SqlCommand cmdBlockUser = new SqlCommand("UPDATE Usuario SET Estado = 0 WHERE username = @Username", conn);
                    cmdBlockUser.Parameters.AddWithValue("@Username", username);
                    cmdBlockUser.ExecuteNonQuery();

                    lblMessage.Text = "Has ingresado incorrectamente tu contraseña tres veces. Tu cuenta ha sido bloqueada.";
                }
                else
                {
                    // Aquí se muestra la alerta de JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "mostrarAlerta();", true);

                    // Aquí se establece el mensaje para mostrar en caso de usuario o contraseña incorrectos
                    lblMessage.Text = "Usuario o contraseña incorrectos.";
                }
            }
        }
    }

    private void LogException(Exception ex)
    {
        // Aquí implementa el código para registrar la excepción en un sistema de registro
    }
}
