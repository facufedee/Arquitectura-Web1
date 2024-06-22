using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Logs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            // Redirigir a la página de inicio de sesión si no está autenticado
            Response.Redirect("IniciarSesion.aspx");
        }


        CargarEventosDeSesion();   
    }
    protected void dgvEventos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvEventos.PageIndex = e.NewPageIndex;
        CargarEventosDeSesion();
    }
    private void CargarEventosDeSesion()
        {
        if (Session["Username"] != null)
        {
            string username = Session["Username"].ToString();
            string connectionString = "Data Source=Facanda\\SQLEXPRESS;Initial Catalog=TallerOesteWeb;Integrated Security=True;";
            string query = "SELECT FechaHora, Evento FROM Bitacora WHERE Username = @Username ORDER BY FechaHora DESC"; // Ajusta la consulta según tu estructura de base de datos

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);

                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvEventos.DataSource = dataTable;
                dgvEventos.DataBind();
            }
        }
        else
        {
            // Manejar el caso en que no haya un nombre de usuario en la sesión
            // Por ejemplo, redirigir a la página de inicio de sesión
            Response.Redirect("Login.aspx");
        }
    }
}
