using System;

namespace NotesSharingWebForms
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isLoggedIn = Session["UserId"] != null;

            if (isLoggedIn)
            {
                // ✅ Show for logged-in users
                LoginLink.Visible = false;
                RegisterLink.Visible = false;
                AllNotesLink.Visible = true;
                DashboardLink.Visible = true;
                UploadLink.Visible = true;
                ProfileLink.Visible = true;
                

                // Show Welcome message
                litWelcome.Text = $"<span class='welcome'>Welcome, {Session["UserName"]}</span>";
            }
            else
            {
                // ❌ Show for visitors
                LoginLink.Visible = true;
                RegisterLink.Visible = true;
                AllNotesLink.Visible = true;
                DashboardLink.Visible = false;
                UploadLink.Visible = false;
                

                litWelcome.Text = "";
            }
        }
    }
}
