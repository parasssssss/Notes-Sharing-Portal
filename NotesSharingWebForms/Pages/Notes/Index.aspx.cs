using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace NotesSharingWebForms.Pages.Notes
{
    public partial class Index : System.Web.UI.Page
    {
        string ConnStr => System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null) Response.Redirect("../Account/Login.aspx");
            if (!IsPostBack) LoadMyNotes();
        }

        void LoadMyNotes()
        {
            using (var conn = new MySqlConnection(ConnStr))
            using (var da = new MySqlDataAdapter("SELECT id, title, file_path, upload_time, visibility FROM notes WHERE uploaded_by = @u ORDER BY upload_time DESC;", conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@u", Convert.ToInt32(Session["UserId"]));
                var dt = new DataTable();
                da.Fill(dt);
                gvMyNotes.DataSource = dt;
                gvMyNotes.DataBind();
            }
        }
    }
}
