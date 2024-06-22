<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registro.aspx.cs" Inherits="Registro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro</title>
    <link rel="stylesheet" href="css/styles-sesion.css" />
</head>
<body>
    <form id="registerForm" action="ProcesarRegistro.aspx" method="post" runat="server" class="container">
        <h2>Registro</h2>
        <input type="text" id="username" name="username" placeholder="Usuario" required />
        <input type="email" id="email" name="email" placeholder="Correo Electrónico" required />
        <input type="password" id="password" name="password" placeholder="Contraseña" required />
        <input type="password" id="confirmPassword" name="confirmPassword" placeholder="Confirmar Contraseña" required />
        <input type="submit" value="Registrar" />
        <div id="errorMessage" class="error-message" style="display: none;"></div>
        <div>Ya tienes una cuenta? <a href="IniciarSesion.aspx">Inicia sesión aquí</a></div>
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </form>

    <script src="js/main.js"></script>
</body>
</html>
