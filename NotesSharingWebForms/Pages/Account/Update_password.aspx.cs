using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;

namespace NotesSharingWebForms.Pages.Account
{
    public partial class Update_Password : System.Web.UI.Page
    {
        string ConnStr => System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Optional: check if token exists
            if (!IsPostBack)
            {
                string token = Request.QueryString["token"];
                if (string.IsNullOrEmpty(token))
                {
                    litMsg.Text = "<div class='alert'>Invalid reset link.</div>";
                    btnUpdate.Enabled = false;
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];
            string newPassword = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(newPassword))
            {
                litMsg.Text = "<div class='alert'>Please enter a new password.</div>";
                return;
            }

            string hashedPassword = HashPassword(newPassword);

            using (var conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "UPDATE users SET password=@p, reset_token=NULL, reset_expiry=NULL WHERE reset_token=@t AND reset_expiry > NOW()",
                    conn);
                cmd.Parameters.AddWithValue("@p", hashedPassword);
                cmd.Parameters.AddWithValue("@t", token);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                    litMsg.Text = "<div class='alert'>Password updated successfully! You can now login.</div>";
                else
                    litMsg.Text = "<div class='alert'>Invalid or expired reset link.</div>";
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (var b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
