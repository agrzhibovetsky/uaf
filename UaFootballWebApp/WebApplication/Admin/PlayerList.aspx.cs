using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;

namespace UaFootball.WebApplication
{
    public partial class PlayerList : ObjectListPageBase<PlayerDTO>
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
            dgData.DataSource = new PlayerDTOHelper().GetFromDB("А", Constants.QueryType.StartsWith, false, false);
            dgData.DataBind();
        }

        protected void btnLetter_Command(object sender, CommandEventArgs e)
        {
            string letter = e.CommandArgument.ToString();
            dgData.DataSource = new PlayerDTOHelper().GetFromDB(letter, Constants.QueryType.StartsWith, false, false);
            dgData.DataBind();
        }

    } 
}