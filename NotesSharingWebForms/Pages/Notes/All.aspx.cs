using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace NotesSharingWebForms.Pages.Notes
{
    public partial class All : System.Web.UI.Page
    {
        string ConnStr => System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) LoadAll();
        }

        void LoadAll()
        {
            string sql = @"SELECT n.id, n.title, n.file_path, n.upload_time, n.visibility, u.name AS uploader
FROM notes n 
INNER JOIN users u ON n.uploaded_by = u.id
WHERE n.visibility = 'Public'
ORDER BY n.upload_time DESC;

";
            using (var conn = new MySqlConnection(ConnStr))
            using (var da = new MySqlDataAdapter(sql, conn))
            {
                var dt = new DataTable();
                da.Fill(dt);
                gvMyNotes.DataSource = dt;
                gvMyNotes.DataBind();
            }
        }
    }
}
