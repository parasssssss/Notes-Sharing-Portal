<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forgot_password.aspx.cs" Inherits="NotesSharingWebForms.Pages.Account.Forgot_password" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="input" Placeholder="Enter your email"></asp:TextBox>
<asp:Button ID="btnSend" runat="server" Text="Send Reset Link" OnClick="btnSend_Click" CssClass="btn btn-primary" />
<asp:Literal ID="litMsg" runat="server"></asp:Literal>

        </div>
    </form>
</body>
</html>
