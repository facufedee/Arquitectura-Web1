<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgregarVehiculo.aspx.cs" Inherits="AgregarVehiculo" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Agregar Vehículo</title>
    <link rel="stylesheet" type="text/css" href="css/styles-dashboard.css" />
    <link rel="stylesheet" type="text/css" href="css/styles-agregarVehiculo.css" />
</head>
<body>
    <form id="formAgregarVehiculo" runat="server" action="ProcesarAgregarVehiculo.aspx" method="post">
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
                    <label for="marca">Marca:</label>
                    <input type="text" id="marca" name="marca" required><br>

                    <label for="modelo">Modelo:</label>
                    <input type="text" id="modelo" name="modelo" required><br>

                    <label for="anio">Año:</label>
                    <input type="number" id="anio" name="anio" min="1900" max="2099" required><br>

                    <label for="color">Color:</label>
                    <input type="text" id="color" name="color" required><br>

                    <label for="patente">Patente:</label>
                    <input type="text" id="patente" name="patente" maxlength="20" required><br>

                    <input type="submit" value="Agregar Vehículo">
                </div>
                <div class="vehicle-list">
                    <h2>Tus Vehículos</h2>
                    <asp:GridView ID="gvVehiculosUsuario" runat="server" CssClass="gridview" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="Marca" HeaderText="Marca" />
                            <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                            <asp:BoundField DataField="Año" HeaderText="Año" />
                            <asp:BoundField DataField="Color" HeaderText="Color" />
                            <asp:BoundField DataField="Patente" HeaderText="Patente" />
                        </Columns>
                    </asp:GridView>
                </div>
            </main>
        </div>
    </form>
</body>
</html>
