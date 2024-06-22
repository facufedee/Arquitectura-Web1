    <%@ Page Language="C#" AutoEventWireup="true" CodeFile="IniciarSesion.aspx.cs" Inherits="Iniciar_Sesion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Iniciar Sesión</title>
    <link rel="stylesheet" href="css/styles-sesion.css" />
</head>
<body>
    <form id="loginForm" action="ProcesarSesion.aspx" method="post" runat="server" class="container">
        <h2>Iniciar Sesión</h2>
        <input type="text" id="username" name="username" placeholder="Usuario" required />
        <input type="password" id="password" name="password" placeholder="Contraseña" required />
        <input type="submit" value="Iniciar Sesión" />
        <div id="errorMessage" class="error-message" style="display: none;"></div>
        <div>No tienes una cuenta? <a href="Registro.aspx">Regístrate aquí</a></div>
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </form>

    <script src="js/main.js"></script>
</body>
</html>
