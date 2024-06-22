<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SolicitarTurno.aspx.cs" Inherits="SolicitarTurno" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Panel de Turnos</title>
    <link rel="stylesheet" type="text/css" href="css/styles-dashboard.css" />
    <link rel="stylesheet" type="text/css" href="css/styles-turnos.css" />
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
                    <h2>Pedir Turnos</h2>
                    <asp:DropDownList ID="ddlVehiculos" runat="server" CssClass="dropdownlist"></asp:DropDownList>
                    <label for="ddlTurnosDisponibles">Fecha</label>
                    <asp:DropDownList ID="ddlTurnosDisponibles" runat="server" CssClass="dropdown"></asp:DropDownList>
                    <label for="ddlSucursal">Sucursal</label>
                    <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="dropdown"></asp:DropDownList>
                    <div class="buttons">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" OnClick="btnGuardar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" />
                    </div>
                    <asp:GridView ID="gvTurnos" runat="server" CssClass="gridview" AutoGenerateColumns="False" OnRowCommand="gvTurnos_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="TurnoID" HeaderText="TurnoID" />
                            <asp:BoundField DataField="Marca" HeaderText="Marca" />
                            <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                            <asp:BoundField DataField="Sucursal" HeaderText="Sucursal" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnCancelarTurno" runat="server" Text="Cancelar Turno" CommandName="CancelarTurno" CommandArgument='<%# Eval("TurnoID") %>' CssClass="button" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
                </div>
            </main>
        </div>
    </form>
</body>
</html>
