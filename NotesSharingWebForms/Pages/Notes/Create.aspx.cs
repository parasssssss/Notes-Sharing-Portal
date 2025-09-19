using System;
using System.IO;
using MySql.Data.MySqlClient;

namespace NotesSharingWebForms.Pages.Notes
{
    public partial class Create : System.Web.UI.Page
    {
        string ConnStr => System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null) Response.Redirect("../Account/Login.aspx");
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (!fu.HasFile) { litMsg.Text = "<div class='alert'>Please choose a PDF file.</div>"; return; }
            var ext = Path.GetExtension(fu.FileName).ToLowerInvariant();
            if (ext != ".pdf") { litMsg.Text = "<div class='alert'>Only PDF allowed.</div>"; return; }

            var title = txtTitle.Text.Trim();
            if (string.IsNullOrWhiteSpace(title)) { litMsg.Text = "<div class='alert'>Title is required.</div>"; return; }


            var visibility = rbVisibility.SelectedValue;  // 👈 New

            var guid = Guid.NewGuid().ToString();
            var newFileName = guid + ".pdf";
            var physicalPath = Server.MapPath("~/Uploads/" + newFileName);
            fu.SaveAs(physicalPath);

            // Save DB path as lowercase '/uploads/...'
            var dbPath = "/uploads/" + newFileName;

            using (var conn = new MySqlConnection(ConnStr))
            using (var cmd = new MySqlCommand("INSERT INTO notes(title, file_path, uploaded_by, visibility) VALUES(@t, @p, @u, @v)", conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@t", title);
                cmd.Parameters.AddWithValue("@p", dbPath);
                cmd.Parameters.AddWithValue("@u", Convert.ToInt32(Session["UserId"]));
                cmd.Parameters.AddWithValue("@v", visibility);
                cmd.ExecuteNonQuery();
            }

            Response.Redirect("Index.aspx");
        }
    }
}
