using System;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;

namespace NotesSharingWebForms.Pages.Account
{
    public partial class Login : System.Web.UI.Page
    {
        string ConnStr => System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
                Response.Redirect("../Notes/Index.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            using (var conn = new MySqlConnection(ConnStr))
            using (var cmd = new MySqlCommand("SELECT id, password, name FROM users WHERE email=@e", conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@e", email);
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        var id = r.GetInt32("id");
                        var hash = r.GetString("password");
                        var name = r.GetString("name");

                        // Hash entered password and compare
                        if (hash == HashPassword(password))
                        {
                            // Login OK
                            Session["UserId"] = id;
                            Session["UserName"] = name;
                            FormsAuthentication.SetAuthCookie(email, false);

                            r.Close();

                            using (var cmd2 = new MySqlCommand("INSERT INTO sessions(user_id, ip_address) VALUES(@u, @ip); SELECT LAST_INSERT_ID();", conn))
                            {
                                cmd2.Parameters.AddWithValue("@u", id);
                                cmd2.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress ?? "");
                                var sessId = Convert.ToInt32(cmd2.ExecuteScalar());
                                Session["SessRowId"] = sessId;
                            }

                            Response.Redirect("../Notes/Index.aspx");
                            return;
                        }
                    }
                }
            }

            litMsg.Text = "<div class='alert'>Invalid email or password</div>";
        }

        // Same SHA256 hashing method as in Register
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Account/Register.aspx");
        }
    }
}
