using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.DB;

namespace UaFootball.WebApplication.Public
{
    public partial class Country : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (Request["countryId"] != null)
            {
                int countryId = 0;
                if (int.TryParse(Request["countryId"], out countryId))
                {
                    using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
                    {
                        List<Match> games = db.Matches.Where(m => m.NationalTeam.Country_Id == countryId || m.NationalTeam1.Country_Id == countryId).ToList();
                        Repeater rptMatches = accCountry.Panes["apMatches"].FindControl("rptMatches") as Repeater;
                        rptMatches.DataSource = games;
                        rptMatches.DataBind();

                        List<DB.Club> clubs = db.Clubs.Where(c => c.City.Country_ID == countryId).ToList();
                        Repeater rptClubs = accCountry.Panes["apClubs"].FindControl("rptClubs") as Repeater;
                        rptClubs.DataSource = clubs;
                        rptClubs.DataBind();

                        List<UaFootball.DB.Player> players = db.Players.Where(p => p.Country_Id == countryId).OrderBy(p=>p.Last_Name).ToList();
                        Repeater rptPlayers = accCountry.Panes["apPlayers"].FindControl("rptPlayers") as Repeater;
                        rptPlayers.DataSource = players;
                        rptPlayers.DataBind();
                    }
                }
            }
        }
    }
}