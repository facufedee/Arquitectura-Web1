<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
    <link rel="stylesheet" type="text/css" href="css/styles-dashboard.css" />
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
<div class="background-image"></div>            </main>
        </div>

    </form>
</body>
</html>
