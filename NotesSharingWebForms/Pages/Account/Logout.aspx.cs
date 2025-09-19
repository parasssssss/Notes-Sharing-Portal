using System;
using System.Web;
using System.Web.Security;

namespace NotesSharingWebForms.Pages.Account
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear session
            Session.Clear();
            Session.Abandon();

            // Sign out from FormsAuthentication
            FormsAuthentication.SignOut();

            // Redirect to login page
            Response.Redirect("Login.aspx");
        }
    }
}
