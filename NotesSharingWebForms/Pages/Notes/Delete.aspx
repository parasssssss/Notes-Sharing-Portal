<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="NotesSharingWebForms.Pages.Notes.Delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2>Delete Note</h2>
        <asp:Literal ID="litMsg" runat="server" />

        <p><strong>Are you sure you want to delete this note?</strong></p>
        <p><strong>ID:</strong> <asp:Literal ID="litId" runat="server" /></p>
        <p><strong>Title:</strong> <asp:Literal ID="litTitle" runat="server" /></p>

        <asp:HiddenField ID="hidId" runat="server" />
        <asp:HiddenField ID="hidPath" runat="server" />

        <br />
        <asp:Button ID="btnConfirm" runat="server" Text="Yes, Delete" CssClass="btn btn-danger" OnClick="btnConfirm_Click" />
        <a class="btn" href="Index.aspx">Cancel</a>
    </div>
</asp:Content>
