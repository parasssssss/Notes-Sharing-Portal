<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="NotesSharingWebForms.Pages.Account.Register" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Register</title>
    <link href="../../Styles/site.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
    <div class="container">
        <h2>Register</h2>
        <asp:Literal ID="litMsg" runat="server" />
        <label>Name</label>
        <asp:TextBox ID="txtName" runat="server" CssClass="input" />
        <label>Email</label>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="input" />
        <label>Password</label>
        <asp:TextBox ID="txtPassword" runat="server" CssClass="input" TextMode="Password" />
        <br /><br />
        <asp:Button ID="btnRegister" runat="server" Text="Create Account" CssClass="btn btn-primary" OnClick="btnRegister_Click" />
        <a class="btn" href="Login.aspx">Back to Login</a>
    </div>
</form>
</body>
</html>
<%--  --%>