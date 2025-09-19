<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_Password.aspx.cs" Inherits="NotesSharingWebForms.Pages.Account.Update_Password" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="input" TextMode="Password" Placeholder="New Password"></asp:TextBox>
<asp:Button ID="btnUpdate" runat="server" Text="Update Password" OnClick="btnUpdate_Click" CssClass="btn btn-primary" />
<asp:Literal ID="litMsg" runat="server"></asp:Literal>

        </div>
    </form>
</body>
</html>
