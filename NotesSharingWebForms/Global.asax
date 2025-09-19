<%@ Application Language="C#" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="MySql.Data.MySqlClient" %>

<script runat="server">
    void Application_Start(object sender, EventArgs e)
    {
        // Ensure physical Uploads folder exists
        var uploads = Server.MapPath("~/Uploads");
        if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Nothing here; we insert into sessions table on successful login and store sessions row id in Session["SessRowId"]
    }

    void Session_End(object sender, EventArgs e)
    {
        // If logged in and had a sessions row, update logout_time
        try
        {
            var rowId = Session["SessRowId"] as int?;
            if (rowId.HasValue)
            {
                using (var conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString))
                using (var cmd = new MySqlCommand("UPDATE sessions SET logout_time=NOW() WHERE id=@id", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", rowId.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch { /* swallow */ }
    }
</script>
