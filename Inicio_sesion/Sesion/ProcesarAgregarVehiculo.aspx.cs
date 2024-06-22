using System;
using System.Data.SqlClient;
using System.Web.UI;

public partial class ProcesarAgregarVehiculo : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            // Redirigir a la página de inicio de sesión si no está autenticado
            Response.Redirect("IniciarSesion.aspx");
        }
        if (!IsPostBack)
        {
            ProcesarAgregarVeh();
        }
    }

    private void ProcesarAgregarVeh()
    {
        try
        {
            // Obtener los datos enviados por POST desde el formulario
            string username = Session["username"].ToString();
            string marca = Request.Form["marca"];
            string modelo = Request.Form["modelo"];
            int anio = Convert.ToInt32(Request.Form["anio"]);
            string color = Request.Form["color"];
            string patente = Request.Form["patente"];

            // Cadena de conexión a la base de datos
            string connectionString = "Data Source=Facanda\\SQLEXPRESS;Initial Catalog=TallerOesteWeb;Integrated Security=True;";

            // Consulta SQL para insertar el vehículo en la tabla Vehiculos
            string query = "INSERT INTO Vehiculos (Marca, Modelo, Año, Color, Patente, username) VALUES (@Marca, @Modelo, @Año, @Color, @Patente, @username)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Establecer los parámetros
                    command.Parameters.AddWithValue("@Marca", marca);
                    command.Parameters.AddWithValue("@Modelo", modelo);
                    command.Parameters.AddWithValue("@Año", anio);
                    command.Parameters.AddWithValue("@Color", color);
                    command.Parameters.AddWithValue("@Patente", patente);
                    command.Parameters.AddWithValue("@username", username);

                    DateTime fechaHora = DateTime.Now;
                    string evento = "Registro de vehiculo exitoso"; // Define el evento a registrar
                    RegistroEventos(username, evento, fechaHora); // Aquí cambia la llamada

                    // Abrir la conexión y ejecutar la consulta
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // Redireccionar a una página de éxito o mostrar un mensaje
            Response.Redirect("AgregarVehiculo.aspx");
        }
        catch (Exception ex)
        {
            // Manejar errores
            // Por ejemplo, podrías mostrar un mensaje de error en la página
           // lblError.Text = "Ocurrió un error al procesar la solicitud: " + ex.Message;
            //lblError.Visible = true;
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

}
