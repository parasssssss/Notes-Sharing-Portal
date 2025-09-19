using System;
using System.Net.Mail;
using MySql.Data.MySqlClient;

namespace NotesSharingWebForms.Pages.Account
{
    public partial class Forgot_password : System.Web.UI.Page
    {
        string ConnStr => System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string token = Guid.NewGuid().ToString();
            DateTime expiry = DateTime.Now.AddHours(1);

            using (var conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE users SET reset_token=@t, reset_expiry=@e WHERE email=@mail", conn);
                cmd.Parameters.AddWithValue("@t", token);
                cmd.Parameters.AddWithValue("@e", expiry);
                cmd.Parameters.AddWithValue("@mail", email);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    string resetLink = "https://localhost:44340/Pages/Account/Update_Password.aspx?token=" + token;
                    SendEmail(email, resetLink);
                    litMsg.Text = "<div class='alert'>Check your email for a reset link.</div>";
                }
                else
                {
                    litMsg.Text = "<div class='alert'>Email not found.</div>";
                }
            }
        }

        void SendEmail(string to, string link)
        {
            MailMessage mail = new MailMessage("info.notesharing@gmail.com", to);
            mail.Subject = "Password Reset";
            mail.Body = "Click the link to reset your password: " + link;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential("info.notesharing@gmail.com", "cpcq brzo oznn epwq");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}
