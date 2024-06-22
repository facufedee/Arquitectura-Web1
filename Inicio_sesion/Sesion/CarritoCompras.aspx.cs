using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CarritoCompras : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            // Redirigir a la página de inicio de sesión si no está autenticado
            Response.Redirect("IniciarSesion.aspx");
        }

        if (Request.HttpMethod == "POST")
        {
            string producto = Request.Form["producto"];
            string precio = Request.Form["precio"];

            // Verificar si la sesión ya existe o crear una nueva lista si no existe
            List<Producto> carrito = Session["Carrito"] as List<Producto>;
            if (carrito == null)
            {
                carrito = new List<Producto>();
                Session["Carrito"] = carrito;
            }

            // Agregar el producto al carrito
            carrito.Add(new Producto { Nombre = producto, Precio = precio });
        }

        // Mostrar el contenido del carrito
        MostrarCarrito();
    }

    private void MostrarCarrito()
    {
        List<Producto> carrito = Session["Carrito"] as List<Producto>;

        if (carrito != null && carrito.Count > 0)
        {
            btnFinalizarCompra.Visible = true; 

            dgvCarrito.DataSource = carrito;
            dgvCarrito.DataBind();

        }
        else
        {
            // Si el carrito está vacío, muestra un mensaje indicando que está vacío
            lblMensaje.Text = "Tu carrito de compras está vacío.";
        }
    }

    public class Producto
    {
        public string Nombre { get; set; }
        public string Precio { get; set; }
    }
    protected void btnFinalizarCompra_Click(object sender, EventArgs e)
    {
        List<Producto> carrito = Session["Carrito"] as List<Producto>;

        if (carrito != null && carrito.Count > 0)
        {
            string usuarioID = Session["username"].ToString(); // Aquí obtienes el ID del usuario, por ejemplo, desde la sesión

            foreach (Producto producto in carrito)
            {
                // Insertar los detalles de la compra en la base de datos
                InsertarDetalleCompra(usuarioID, producto.Nombre, producto.Precio);
            }

            // Limpiar el carrito después de finalizar la compra
            Session["Carrito"] = null;
            lblMensaje.Text = "¡Compra finalizada con éxito!";
            btnFinalizarCompra.Visible = false; // Opcional: Ocultar el botón después de finalizar la compra
        }
        else
        {
            lblMensaje.Text = "El carrito de compras está vacío.";
        }
    }

    private void InsertarDetalleCompra(string usuarioID, string productoNombre, string precio)
    {

        string connectionString = "Data Source=Facanda\\SQLEXPRESS;Initial Catalog=TallerOesteWeb;Integrated Security=True;";
        string query = "INSERT INTO DetalleDeCompras (UsuarioID, ProductoNombre, Precio) VALUES (@UsuarioID, @ProductoNombre, @Precio)";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UsuarioID", usuarioID);
                command.Parameters.AddWithValue("@ProductoNombre", productoNombre);
                command.Parameters.AddWithValue("@Precio", precio);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        
    }

    private string ObtenerIDUsuario()
    {
    
        return "IDUsuarioEjemplo";
    }

}