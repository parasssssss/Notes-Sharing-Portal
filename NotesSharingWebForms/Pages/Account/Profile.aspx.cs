using System;
using System.Web.Security;
using MySql.Data.MySqlClient;

namespace NotesSharingWebForms.Pages.Account
{
    public partial class Profile : System.Web.UI.Page
    {
        string ConnStr => System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            EnsureLoggedIn();
            if (!IsPostBack) LoadProfile();
        }

        void EnsureLoggedIn()
        {
            if (Session["UserId"] == null) Response.Redirect("Login.aspx");
        }

        void LoadProfile()
        {
            using (var conn = new MySqlConnection(ConnStr))
            using (var cmd = new MySqlCommand("SELECT name, email FROM users WHERE id=@id", conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(Session["UserId"]));
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        litName.Text = r.GetString("name");
                        litEmail.Text = r.GetString("email");
                    }
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Update sessions.logout_time if we have the row id
            try
            {
                var rowId = Session["SessRowId"] as int?;
                if (rowId.HasValue)
                {
                    using (var conn = new MySqlConnection(ConnStr))
                    using (var cmd = new MySqlCommand("UPDATE sessions SET logout_time=NOW() WHERE id=@id", conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("@id", rowId.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }

            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("../../Default.aspx");
        }
    }
}
