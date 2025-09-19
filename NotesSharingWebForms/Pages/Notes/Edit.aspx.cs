using System;
using System.IO;
using MySql.Data.MySqlClient;

namespace NotesSharingWebForms.Pages.Notes
{
    public partial class Edit : System.Web.UI.Page
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
            hidId.Value = id.ToString();

            using (var conn = new MySqlConnection(ConnStr))
            using (var cmd = new MySqlCommand("SELECT title, file_path, uploaded_by FROM notes WHERE id=@id", conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id", id);
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        // only owner can edit
                        int owner = Convert.ToInt32(r["uploaded_by"]);
                        if (owner != Convert.ToInt32(Session["UserId"])) Response.Redirect("Index.aspx");

                        txtTitle.Text = r["title"].ToString();
                        ViewState["OldFilePath"] = r["file_path"].ToString();
                    }
                    else Response.Redirect("Index.aspx");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(hidId.Value, out int id)) { Response.Redirect("Index.aspx"); return; }
            string newTitle = txtTitle.Text.Trim();
            if (newTitle == "") { litMsg.Text = "<div class='alert'>Title is required.</div>"; return; }

            string filePath = ViewState["OldFilePath"] as string ?? "";
            var visibility = rbVisibility.SelectedValue;

            if (fu.HasFile)
            {
                var ext = Path.GetExtension(fu.FileName).ToLowerInvariant();
                if (ext != ".pdf") { litMsg.Text = "<div class='alert'>Only PDF allowed.</div>"; return; }

                // delete old
                if (!string.IsNullOrEmpty(filePath))
                {
                    var physicalOld = Server.MapPath("~" + filePath.Replace("/", "\\"));
                    if (File.Exists(physicalOld)) File.Delete(physicalOld);
                }

                // save new
                var guid = Guid.NewGuid().ToString() + ".pdf";
                var physicalNew = Server.MapPath("~/Uploads/" + guid);
                fu.SaveAs(physicalNew);
                filePath = "/uploads/" + guid;
            }

            using (var conn = new MySqlConnection(ConnStr))
            using (var cmd = new MySqlCommand("UPDATE notes SET title=@t, file_path=@p ,Visibility=@v  WHERE id=@id AND uploaded_by=@u", conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@t", newTitle);
                cmd.Parameters.AddWithValue("@p", filePath);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@v", visibility);
                cmd.Parameters.AddWithValue("@u", Convert.ToInt32(Session["UserId"]));

                var rows = cmd.ExecuteNonQuery();
                if (rows == 0) { litMsg.Text = "<div class='alert'>Update failed or not authorized.</div>"; return; }
            }

            Response.Redirect("Index.aspx");
        }
    }
}
