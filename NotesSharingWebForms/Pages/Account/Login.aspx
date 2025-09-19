<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NotesSharingWebForms.Pages.Account.Login" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login</title>
    <link href="../../Styles/site.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
    <div class="container">
        <h2>Login</h2>
        <asp:Literal ID="litMsg" runat="server" />
        <label>Email</label>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="input" />
        <label>Password</label>
        <asp:TextBox ID="txtPassword" runat="server" CssClass="input" TextMode="Password" />
        <br /><br />
        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
        <a class="btn" href="Register.aspx">Register</a>
    </div>
</form>
</body>
</html>
