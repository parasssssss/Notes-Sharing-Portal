using System;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;

namespace NotesSharingWebForms.Pages.Account
{
    public partial class Register : System.Web.UI.Page
    {
        string ConnStr => System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            var email = txtEmail.Text.Trim();
            var pass = txtPassword.Text;

            if (name == "" || email == "" || pass == "")
            {
                litMsg.Text = "<div class='alert'>All fields are required.</div>";
                return;
            }

            using (var conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                // Check unique email
                using (var chk = new MySqlCommand("SELECT COUNT(*) FROM users WHERE email=@e", conn))
                {
                    chk.Parameters.AddWithValue("@e", email);
                    var exists = Convert.ToInt32(chk.ExecuteScalar()) > 0;
                    if (exists)
                    {
                        litMsg.Text = "<div class='alert'>Email already exists.</div>";
                        return;
                    }
                }

                // Insert new user
                using (var cmd = new MySqlCommand("INSERT INTO users(name,email,password) VALUES(@n,@e,@p)", conn))
                {
                    cmd.Parameters.AddWithValue("@n", name);
                    cmd.Parameters.AddWithValue("@e", email);
                    cmd.Parameters.AddWithValue("@p", HashPassword(pass));
                    cmd.ExecuteNonQuery();
                }
            }
            litMsg.Text = "<div class='alert'>Account created. <a href='Login.aspx'>Login</a></div>";
        }

        // Simple SHA256 hashing method
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
