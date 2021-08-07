using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFDatabase;
using UaFootball.AppCode;

namespace UaFootball.WebApplication.Public
{
    public partial class UkraineRegions : UaFootballPageBase
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void upRegionPlayers_Load(object sender, EventArgs e)
        {

        }

        protected void btnActivateRegion_Click(object sender, EventArgs e)
        {
            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                ltRegionName.Text = hdnRegionName.Value;
                string regionName = hdnRegionName.Value;
                List<UaFDatabase.Player> players = db.Players.Where(p => p.Country.Country_Code == Constants.CountryCodeUA && p.UARegion_Name == regionName).OrderBy(p=>p.UACity_Name).ThenBy(p=>p.Last_Name).ToList();
                rptPlayers.DataSource = players;
                rptPlayers.DataBind();
            }
        }
    }
}