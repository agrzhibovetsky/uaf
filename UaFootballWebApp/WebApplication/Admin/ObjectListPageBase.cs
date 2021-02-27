using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using UaFootball.AppCode;

/// <summary>
/// Summary description for ObjectListPageBase
/// </summary>
namespace UaFootball.WebApplication
{
    public abstract class ObjectListPageBase<T> : UaFootballPageBase where T : class, new()
    {

        protected abstract DataGrid dGrid { get; }

        protected abstract string EditPage { get; }

        protected IDTOHelper<T> DTOHelper { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
            base.OnLoad(e);
        }

        public virtual void BindData()
        {
            IEnumerable<T> data = DTOHelper.GetAllFromDB();
            dGrid.DataSource = data;
            dGrid.DataBind();
        }

        protected void EditObject(object sender, CommandEventArgs e)
        {
            Query.Add(Constants.QueryParam.ObjectId, (string)e.CommandArgument);
            Response.Redirect(string.Concat(EditPage, GetQueryString()));
        }

        protected void DeleteObject(object sender, CommandEventArgs e)
        {
            int countryID = int.Parse((string)e.CommandArgument);
            DTOHelper.DeleteFromDB(countryID);
            BindData();
        }

        protected void AddObject(object sender, EventArgs e)
        {
            Response.Redirect(EditPage);
        }

        public ObjectListPageBase()
        {
            DTOHelper = CreateDTOHelper<T>();
        }
    } 
}