<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logs.aspx.cs" Inherits="Logs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
    <link rel="stylesheet" type="text/css" href="css/styles-dashboard.css" />
    <link rel="stylesheet" type="text/css" href="css/styles-logs.css" />
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
            </aside>
            <main class="main-content">
                <div class="form-container">
                    <h2>Logs de Sesión</h2>
                    <asp:GridView ID="dgvEventos" runat="server" CssClass="gridview" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="dgvEventos_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="FechaHora" HeaderText="Fecha y Hora" />
                            <asp:BoundField DataField="Evento" HeaderText="Evento" />
                        </Columns>
                        <PagerSettings Mode="NextPrevious" NextPageText="Siguiente" PreviousPageText="Anterior" />
                        <PagerStyle CssClass="pager" />
                    </asp:GridView>
                </div>
            </main>
        </div>
    </form>
</body>
</html>
