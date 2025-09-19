<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="NotesSharingWebForms.Pages.Notes.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2>Note Details</h2>

        <p><strong>ID:</strong> <asp:Literal ID="litId" runat="server" /></p>
        <p><strong>Title:</strong> <asp:Literal ID="litTitle" runat="server" /></p>
        <p><strong>File:</strong> <asp:HyperLink ID="lnkFile" runat="server" Text="Download PDF" Target="_blank" /></p>
        <p><strong>Uploaded By:</strong> <asp:Literal ID="litUploader" runat="server" /></p>
        <p><strong>Uploaded At:</strong> <asp:Literal ID="litTime" runat="server" /></p>

        <p>
            <a class="btn" href="All.aspx">Back to All</a>
            <a class="btn" href="Index.aspx">Back to My Notes</a>
        </p>
    </div>
</asp:Content>
