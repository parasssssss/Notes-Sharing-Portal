using System;
using System.IO;
using MySql.Data.MySqlClient;

namespace NotesSharingWebForms.Pages.Notes
{
    public partial class Delete : System.Web.UI.Page
    {
        string ConnStr => System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null) Response.Redirect("../Account/Login.aspx");
            if (!IsPostBack) LoadNote();
        }

        void LoadNote()
        {
            if (!int.TryParse(Request.QueryString["id"], out int id)) { Response.Redirect("Index.aspx"); return; }

            using (var conn = new MySqlConnection(ConnStr))
            using (var cmd = new MySqlCommand("SELECT title, file_path, uploaded_by FROM notes WHERE id=@id", conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id", id);
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        int owner = Convert.ToInt32(r["uploaded_by"]);
                        if (owner != Convert.ToInt32(Session["UserId"])) Response.Redirect("Index.aspx");

                        litId.Text = id.ToString();
                        litTitle.Text = r["title"].ToString();
                        hidId.Value = id.ToString();
                        hidPath.Value = r["file_path"].ToString();
                    }
                    else Response.Redirect("Index.aspx");
                }
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(hidId.Value, out int id)) { Response.Redirect("Index.aspx"); return; }
            string path = hidPath.Value;

            using (var conn = new MySqlConnection(ConnStr))
            using (var cmd = new MySqlCommand("DELETE FROM notes WHERE id=@id AND uploaded_by=@u", conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@u", Convert.ToInt32(Session["UserId"]));
                var rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    // remove file
                    if (!string.IsNullOrEmpty(path))
                    {
                        var physical = Server.MapPath("~" + path.Replace("/", "\\"));
                        if (File.Exists(physical)) File.Delete(physical);
                    }
                    Response.Redirect("Index.aspx");
                    return;
                }
            }
            litMsg.Text = "<div class='alert'>Delete failed.</div>";
        }
    }
}
