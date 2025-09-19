<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="NotesSharingWebForms.Pages.Notes.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2>Edit Note</h2>
        <asp:Literal ID="litMsg" runat="server" />
        <asp:HiddenField ID="hidId" runat="server" />

        <label>Title</label>
        <asp:TextBox ID="txtTitle" runat="server" CssClass="input" />

        <label>Replace PDF (optional)</label>
        <asp:FileUpload ID="fu" runat="server" />
        <br /><br />

               <asp:RadioButtonList ID="rbVisibility" runat="server" CssClass="radio-buttons"
    RepeatLayout="Flow" RepeatDirection="Horizontal">
    <asp:ListItem Value="Public" Selected="True">Public</asp:ListItem>
    <asp:ListItem Value="Private">Private</asp:ListItem>
</asp:RadioButtonList>



        <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        <a class="btn" href="Index.aspx">Back</a>
    </div>
</asp:Content>
