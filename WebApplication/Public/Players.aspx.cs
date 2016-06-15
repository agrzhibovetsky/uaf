using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.DB;
using UaFootball.AppCode;

namespace UaFootball.WebApplication
{
    public partial class Players : UaFootballPageBase
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptPlayers.DataSource = new PlayerDTOHelper().GetFromDB("А", Constants.QueryType.StartsWith, true, false);
                rptPlayers.DataBind();
            }
        }

        protected void btnLetter_Command(object sender, CommandEventArgs e)
        {
            string letter = e.CommandArgument.ToString();
            rptPlayers.DataSource = new PlayerDTOHelper().GetFromDB(letter, Constants.QueryType.StartsWith, true, false);
            rptPlayers.DataBind();
        }

        
    }
}