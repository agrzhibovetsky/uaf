using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;

namespace UaFootball.WebApplication.Admin
{
    public partial class PlayerReviewList : ObjectListPageBase<PlayerDTO>
    {
        protected override string EditPage
        {
            get { return Constants.Pages.Edit_Player; }
        }

        protected override DataGrid dGrid
        {
            get
            {
                return dgData;
            }
        }

        public override void BindData()
        {
            dgData.DataSource = new PlayerDTOHelper().GetFromDB("А", Constants.QueryType.All, false, true);
            dgData.DataBind();
        }

       

    } 
}