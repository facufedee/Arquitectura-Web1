using System;
using System.Data;
using System.Data.SqlClient;

public partial class AgregarVehiculo : System.Web.UI.Page
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
            CargarVehiculosUsuario();
        }
    }


    private void CargarVehiculosUsuario()
    {
        // Verificar si hay una sesión de usuario activa
        if (Session["username"] != null)
        {
            // Obtener el nombre de usuario actual desde la sesión
            string username = Session["username"].ToString();

            // Cadena de conexión a la base de datos
            string connectionString = "Data Source=Facanda\\SQLEXPRESS;Initial Catalog=TallerOesteWeb;Integrated Security=True;";

            // Consulta SQL para obtener los vehículos del usuario
            string query = "SELECT Marca, Modelo, Año, Color, Patente FROM Vehiculos WHERE username = @Username";

            // Crear un DataTable para almacenar los resultados de la consulta
            DataTable dataTable = new DataTable();

            // Establecer la conexión y ejecutar la consulta
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Establecer el parámetro
                    command.Parameters.AddWithValue("@Username", username);

                    // Abrir la conexión y ejecutar la consulta
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Cargar los resultados de la consulta en el DataTable
                    dataTable.Load(reader);

                    // Cerrar el lector y la conexión
                    reader.Close();
                    connection.Close();
                }
            }

            // Asignar el DataTable como DataSource del GridView
            gvVehiculosUsuario.DataSource = dataTable;
            gvVehiculosUsuario.DataBind();
        }
    }
}
