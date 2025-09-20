<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="NotesSharingWebForms.Pages.Notes.Create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2>Upload Note (PDF)</h2>
        <asp:Literal ID="litMsg" runat="server" />
        
        <label>Title</label>
        <asp:TextBox ID="txtTitle" runat="server" CssClass="input" />
        
        <label>PDF File</label>
        <asp:FileUpload ID="fu" runat="server" />
<br /><br />
       <asp:RadioButtonList ID="rbVisibility" runat="server" CssClass="radio-buttons"
    RepeatLayout="Flow" RepeatDirection="Horizontal">
    <asp:ListItem Value="Public" Selected="True">Public</asp:ListItem>
    <asp:ListItem Value="Private">Private</asp:ListItem>
</asp:RadioButtonList>



        <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary" OnClick="btnUpload_Click" />
        <asp:Button ID="Button1" runat="server" Text="Back" CssClass="btn btn-primary" OnClick="Button1_Click"  />
    </div>
</asp:Content>
