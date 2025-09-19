<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="NotesSharingWebForms.Pages.Account.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2>My Profile</h2>
        <p><strong>Name:</strong> <asp:Literal ID="litName" runat="server" /></p>
        <p><strong>Email:</strong> <asp:Literal ID="litEmail" runat="server" /></p>
        <p>
            
            <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn btn-danger" OnClick="btnLogout_Click" />
        </p>
    </div>
</asp:Content>
