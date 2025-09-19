<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="All.aspx.cs" Inherits="NotesSharingWebForms.Pages.Notes.All" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2>All Notes</h2>
        <p>
            
        </p>
        <div class="table-wrapper">
        <asp:GridView ID="gvMyNotes" runat="server" AutoGenerateColumns="False" CssClass="table">
    <Columns>
        
        <asp:BoundField DataField="id" HeaderText="ID" />

        
        <asp:BoundField DataField="title" HeaderText="Title" />

        
        <asp:TemplateField HeaderText="File">
            <ItemTemplate>
                <asp:HyperLink ID="hlDownload" runat="server"
                    NavigateUrl='<%# Eval("file_path") %>'
                    CssClass="btn btn-primary"
                    Text="Download"></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>

        
        <asp:BoundField DataField="upload_time" HeaderText="Uploaded" DataFormatString="{0:yyyy-MM-dd HH:mm}" />

        
        <asp:TemplateField HeaderText="View">
            <ItemTemplate>
                <asp:HyperLink ID="hlView" runat="server"
                    NavigateUrl='<%# "Details.aspx?id=" + Eval("id") %>'
                    CssClass="btn btn-primary"
                    Text="Details"></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>

       
        <asp:TemplateField HeaderText="Edit">
            <ItemTemplate>
                <asp:HyperLink ID="hlEdit" runat="server"
                    NavigateUrl='<%# "Edit.aspx?id=" + Eval("id") %>'
                    CssClass="btn btn-primary"
                    Text="Edit"></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>

        
        <asp:TemplateField HeaderText="Delete">
            <ItemTemplate>
                <asp:HyperLink ID="hlDelete" runat="server"
                    NavigateUrl='<%# "Delete.aspx?id=" + Eval("id") %>'
                    CssClass="btn btn-danger"
                    Text="Delete"></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>

     
        <asp:BoundField DataField="visibility" HeaderText="Visibility" />
    </Columns>
</asp:GridView>

            </div>
    </div>
</asp:Content>
