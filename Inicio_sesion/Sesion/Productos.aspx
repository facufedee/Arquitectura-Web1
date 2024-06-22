<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Productos.aspx.cs" Inherits="Productos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MARKET</title>
    <link rel="stylesheet" type="text/css" href="css/styles-dashboard.css" />
    <link rel="stylesheet" type="text/css" href="css/styles-productos.css" />
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
                <div class="producto-tarjeta">
                    <img src="images/selenia.jpg" alt="Lubricante Petronas Selenia K 15w40" class="producto-imagen">
                    <div class="producto-info">
                        <h3 class="producto-titulo">Aceite Castrol Magnatec</h3>
                        <p class="producto-descripcion">Aceite semi-sintético para motores nafteros, diesel livianos y GNC.</p>
                        <p class="producto-precio">$55,000</p>
                        <form action="CarritoCompras.aspx" method="post">
                            <input type="hidden" name="producto" value="Aceite Castrol Magnatec">
                            <input type="hidden" name="precio" value="55.000">
                            <button type="submit" class="btn-agregar-carrito">Agregar al Carrito</button>
                        </form>
                    </div>
                </div>
                <div class="producto-tarjeta">
                    <img src="images/selenia.jpg" alt="Lubricante Petronas Selenia K 15w40" class="producto-imagen">
                    <div class="producto-info">
                        <h3 class="producto-titulo">Shell Helix Ultra</h3>
                        <p class="producto-descripcion">Aceite totalmente sintético para motores nafteros y diesel.</p>
                        <p class="producto-precio">$65,000</p>
                        <form action="CarritoCompras.aspx" method="post">
                            <input type="hidden" name="producto" value="Shell Helix Ultra">
                            <input type="hidden" name="precio" value="65.000">
                            <button type="submit" class="btn-agregar-carrito">Agregar al Carrito</button>
                        </form>
                    </div>
                </div>
            </main>
        </div>
    </form>
</body>
</html>
