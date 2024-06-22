    using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProcesarTurno : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcesarSolicitudTurno();
        }
    }

    private void ProcesarSolicitudTurno()
    {
        try
        {
            // Aquí obtienes los datos enviados por POST desde la página anterior
            string username = Session["Username"].ToString();
            int vehiculoID = Convert.ToInt32(Request.Form["vehiculoID"]);
            int fechaID = Convert.ToInt32(Request.Form["fechaID"]);
            int sucursalID = Convert.ToInt32(Request.Form["sucursalID"]);

            // Cadena de conexión a la base de datos
            string connectionString = "Data Source=Facanda\\SQLEXPRESS;Initial Catalog=TallerOesteWeb;Integrated Security=True;";

            // Consulta SQL para insertar el turno en la tabla Turnos
            string query = "INSERT INTO Turnos (username, VehiculoID, FechaID, SucursalID) VALUES (@Username, @VehiculoID, @FechaID, @SucursalID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Establecer los parámetros
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@VehiculoID", vehiculoID);
                    command.Parameters.AddWithValue("@FechaID", fechaID);
                    command.Parameters.AddWithValue("@SucursalID", sucursalID);

                    // Abrir la conexión y ejecutar la consulta
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // Mostrar un mensaje de éxito
            //lblMessage.Text = "Turno solicitado correctamente.";
            //lblMessage.Visible = true;
        }
        catch (Exception ex)
        {
            // Manejar errores
            //lblMessage.Text = "Ocurrió un error al procesar el turno: " + ex.Message;
            //lblMessage.Visible = true;
        }
    }

}