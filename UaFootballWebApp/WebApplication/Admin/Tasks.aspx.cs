using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFDatabase;
using UaFootball.AppCode;


namespace UaFootball.WebApplication.Admin
{
    public partial class Tasks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (UaFDatabase.UaFootball_DBDataContext db = DBManager.GetDB())
            {
                List<Task> tasks = db.Tasks.ToList();
                rptTasks.DataSource = tasks;
                rptTasks.DataBind();
            }
        }
    }
}