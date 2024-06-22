<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CarritoCompras.aspx.cs" Inherits="CarritoCompras" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Carrito de Compras</title>
    <link rel="stylesheet" type="text/css" href="css/styles-dashboard.css" />
    <link rel="stylesheet" type="text/css" href="css/styles-carrito.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
                        <aside class="sidebar">

<nav>
    <ul>
        <li><a href="SolicitarTurno.aspx">Pedir Turnos</a></li>
        <li><a href="AgregarVehiculo.aspx">Registrar Vehículo</a></li>
        <li><a href="Logs.aspx">Logs</a></li>
        <li><a href="Productos.aspx">Productos</a></li>
        <li><a href="CarritoCompras.aspx">Carrito de Compras</a></li>
    </ul>
</nav>    

<hr /> <!-- Separador horizontal dentro del aside -->
<asp:Label ID="lblUserName" runat="server" Text=""></asp:Label> <!-- Nombre de usuario -->
</aside>
            <main class="main-content">
<div class="carrito-container">
    <h2>Carrito de Compras</h2>
                    <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje"></asp:Label>

    <asp:Label ID="Label1" runat="server" CssClass="mensaje"></asp:Label>
    <asp:GridView ID="dgvCarrito" runat="server" AutoGenerateColumns="False" CssClass="gridview">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" />
        </Columns>
    </asp:GridView>
    <div class="buttons">
        <asp:Button ID="btnFinalizarCompra" runat="server" Text="Finalizar Compra" CssClass="button" OnClick="btnFinalizarCompra_Click" Visible="false" />
    </div>
</div>

            </main>
        </div>
    </form>
</body>

</html>
