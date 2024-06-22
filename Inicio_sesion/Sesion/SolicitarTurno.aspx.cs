using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

public partial class SolicitarTurno : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("IniciarSesion.aspx");
        }


        if (!IsPostBack)
        {
            CargarVehiculosUsuario();
            CargarSucursales();
            CargarTurnosDisponibles();
            CargarTurnosUsuario();
        }
    }

    private void ProcesarSolicitudTurno()
    {
        try
        {
            if (Session["username"] == null)
            {
                lblMessage.Text = "Debe iniciar sesión para solicitar un turno.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string username = Session["username"].ToString();
            int vehiculoID = int.Parse(ddlVehiculos.SelectedValue);
            int fechaID = int.Parse(ddlTurnosDisponibles.SelectedValue);
            int sucursalID = int.Parse(ddlSucursal.SelectedValue);

            string connectionString = "Data Source=Facanda\\SQLEXPRESS;Initial Catalog=TallerOesteWeb;Integrated Security=True;";
            string query = "INSERT INTO Turnos (username, VehiculoID, FechaID, SucursalID) VALUES (@Username, @VehiculoID, @FechaID, @SucursalID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@VehiculoID", vehiculoID);
                    command.Parameters.AddWithValue("@FechaID", fechaID);
                    command.Parameters.AddWithValue("@SucursalID", sucursalID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            lblMessage.Text = "Turno solicitado correctamente.";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            DateTime fechaHora = DateTime.Now;
            string evento = "Registro de Turno exitoso"; // Define el evento a registrar
            RegistroEventos(username, evento, fechaHora);
            CargarTurnosUsuario();

        }
        catch (Exception ex)
        {
            lblMessage.Text = "Ocurrió un error al procesar el turno: " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
            LogException(ex);
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
    private void LogException(Exception ex)
    {
        // Implementa tu lógica de registro de excepciones aquí
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        ProcesarSolicitudTurno();
    }

    private void CargarVehiculosUsuario()
    {
        // Verificar si hay una sesión de usuario activa
        if (Session["username"] != null)
        {
            // Obtener el nombre de usuario actual desde la sesión
            string username = Session["username"].ToString();

            // Lista para almacenar los vehículos del usuario
            List<Vehiculo> vehiculosUsuario = new List<Vehiculo>();

            // Cadena de conexión a la base de datos
            string connectionString = "Data Source=Facanda\\SQLEXPRESS;Initial Catalog=TallerOesteWeb;Integrated Security=True;";

            // Consulta SQL para obtener los vehículos del usuario
            string query = "SELECT VehiculoID, Marca, Modelo FROM Vehiculos WHERE username = @Username";

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

                    // Leer los resultados de la consulta y agregar los vehículos a la lista
                    while (reader.Read())
                    {
                        Vehiculo vehiculo = new Vehiculo();
                        vehiculo.VehiculoID = Convert.ToInt32(reader["VehiculoID"]);
                        vehiculo.Marca = reader["Marca"].ToString();
                        vehiculo.Modelo = reader["Modelo"].ToString();
                        vehiculosUsuario.Add(vehiculo);
                    }

                    // Cerrar el lector y la conexión
                    reader.Close();
                    connection.Close();
                }
            }

            // Asignar la lista de vehículos al DropDownList
            ddlVehiculos.DataSource = vehiculosUsuario;
            ddlVehiculos.DataTextField = "MarcaModelo"; // Esto será el formato "Marca - Modelo"
            ddlVehiculos.DataValueField = "VehiculoID";
            ddlVehiculos.DataBind();
        }
    }

    // Clase para representar un vehículo
    public class Vehiculo
    {
        public int VehiculoID { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string MarcaModelo
        {
            get
            {
                return $"{Marca} - {Modelo}";
            }
        }
    }


    private void CargarSucursales()
    {
        // Cadena de conexión a la base de datos
        string connectionString = "Data Source=Facanda\\SQLEXPRESS;Initial Catalog=TallerOesteWeb;Integrated Security=True;";

        // Consulta SQL para obtener las sucursales
        string query = "SELECT SucursalID, Nombre FROM Sucursales";

        // Establecer la conexión y ejecutar la consulta
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Abrir la conexión y ejecutar la consulta
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Asignar los resultados al DropDownList
                ddlSucursal.DataSource = reader;
                ddlSucursal.DataTextField = "Nombre";
                ddlSucursal.DataValueField = "SucursalID";
                ddlSucursal.DataBind();

                // Cerrar el lector y la conexión
                reader.Close();
                connection.Close();
            }
        }
    }


    private void CargarTurnosDisponibles()
    {
        string connectionString = "Data Source=Facanda\\SQLEXPRESS;Initial Catalog=TallerOesteWeb;Integrated Security=True;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT FechaID, Fecha FROM FechasDisponibles WHERE Disponible = 1 AND Fecha BETWEEN '2024-06-10' AND '2024-09-10' AND DATEPART(dw, Fecha) BETWEEN 2 AND 6";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                ddlTurnosDisponibles.DataSource = reader;
                ddlTurnosDisponibles.DataTextField = "Fecha";
                ddlTurnosDisponibles.DataValueField = "FechaID";
                ddlTurnosDisponibles.DataBind();

                reader.Close();
            }
        }
    }

    private void CargarTurnosUsuario()
    {
        if (Session["username"] != null)
        {
            string username = Session["username"].ToString();

            string connectionString = "Data Source=Facanda\\SQLEXPRESS;Initial Catalog=TallerOesteWeb;Integrated Security=True;";
            string query = @"
                SELECT T.TurnoID, V.Marca, V.Modelo, F.Fecha, S.Nombre AS Sucursal
                FROM Turnos T
                JOIN Vehiculos V ON T.VehiculoID = V.VehiculoID
                JOIN FechasDisponibles F ON T.FechaID = F.FechaID
                JOIN Sucursales S ON T.SucursalID = S.SucursalID
                WHERE T.username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    gvTurnos.DataSource = reader;
                    gvTurnos.DataBind();

                    reader.Close();
                }
            }
        }
    }

    protected void gvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "CancelarTurno")
        {
            int turnoID = Convert.ToInt32(e.CommandArgument);

            string connectionString = "Data Source=Facanda\\SQLEXPRESS;Initial Catalog=TallerOesteWeb;Integrated Security=True;";
            string query = "DELETE FROM Turnos WHERE TurnoID = @TurnoID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TurnoID", turnoID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            string username = Session["username"].ToString();
            DateTime fechaHora = DateTime.Now;
            string evento = "Turno Cancelado"; // Define el evento a registrar
            RegistroEventos(username, evento, fechaHora);
            lblMessage.Text = "Turno cancelado correctamente.";
            lblMessage.ForeColor = System.Drawing.Color.Green;


            CargarTurnosUsuario();
        }
    }
}
