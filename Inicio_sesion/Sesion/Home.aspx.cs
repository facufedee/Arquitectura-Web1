using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            string username = Session["username"] as string;
            lblUserName.Text = "Usuario: " + username;
            // Redirigir a la página de inicio de sesión si no está autenticado
            Response.Redirect("IniciarSesion.aspx");
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
  
        
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {

    }

}