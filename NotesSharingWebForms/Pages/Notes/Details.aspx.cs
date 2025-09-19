using System;
using MySql.Data.MySqlClient;

namespace NotesSharingWebForms.Pages.Notes
{
    public partial class Details : System.Web.UI.Page
    {
        string ConnStr => System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) LoadDetails();
        }

        void LoadDetails()
        {
            if (!int.TryParse(Request.QueryString["id"], out int id)) { Response.Redirect("All.aspx"); return; }

            string sql = @"SELECT n.id, n.title, n.file_path, n.upload_time, u.name AS uploader
                           FROM notes n INNER JOIN users u ON u.id=n.uploaded_by
                           WHERE n.id=@id";
            using (var conn = new MySqlConnection(ConnStr))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id", id);
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        litId.Text = r["id"].ToString();
                        litTitle.Text = r["title"].ToString();
                        lnkFile.NavigateUrl = r["file_path"].ToString();
                        litUploader.Text = r["uploader"].ToString();
                        litTime.Text = Convert.ToDateTime(r["upload_time"]).ToString("yyyy-MM-dd HH:mm");
                    }
                    else
                    {
                        Response.Redirect("All.aspx");
                    }
                }
            }
        }
    }
}
