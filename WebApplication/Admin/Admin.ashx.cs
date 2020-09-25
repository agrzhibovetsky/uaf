using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using UaFootball.DB;

namespace UaFootball.WebApplication.Admin
{
    /// <summary>
    /// Summary description for Admin
    /// </summary>
    public class Admin : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];
            string response = "OK";
            switch (action)
            {
                case "getLatestLineup":
                    {
                        response = GetLatestLineup(context.Request);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            context.Response.Write(response);
        }

        public string GetLatestLineup(HttpRequest request)
        {
            int clubId = 0;
            int nationalTeamId = 0;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string result = "OK";
            if (!string.IsNullOrEmpty(request["clubId"]))
            {
                clubId = int.Parse(request["clubId"]);
            }

            if (clubId > 0)
            {
                using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
                {
                    DB.Match curMatch = db.Matches.Where(m => m.HomeClub_Id == clubId || m.AwayClub_Id == clubId).OrderByDescending(m => m.Date).FirstOrDefault();
                    DB.Match latestMatch = db.Matches.Where(m => m.Match_Id!=curMatch.Match_Id && (m.HomeClub_Id == clubId || m.AwayClub_Id == clubId)).OrderByDescending(m => m.Date).FirstOrDefault();

                    if (latestMatch != null)
                    {
                        bool isHomeTeam = latestMatch.HomeClub_Id == clubId;
                        var tmp = (from matchLineup in db.MatchLineups
                                   where matchLineup.Match_Id == latestMatch.Match_Id && matchLineup.IsHomeTeamPlayer == isHomeTeam && !matchLineup.Coach_Id.HasValue
                                   select new { shirtNumber = matchLineup.ShirtNumber, player = matchLineup.Player });

                        var dbLineup = (from matchLineup in db.MatchLineups
                                      where matchLineup.Match_Id == latestMatch.Match_Id && matchLineup.IsHomeTeamPlayer == isHomeTeam && !matchLineup.Coach_Id.HasValue
                                      select new { shirtNumber = matchLineup.ShirtNumber, flags = matchLineup.Flags, player = matchLineup.Player }).ToList();

                        var uiLineup = dbLineup.Select(d => new { shirtNumber = d.shirtNumber, playerId = d.player.Player_Id, playerFlags = d.flags,  playerName = UIHelper.FormatName(d.player.First_Name, d.player.Last_Name, d.player.Display_Name, d.player.Country_Id) });
                        result = serializer.Serialize(uiLineup);
                    }
                }
            }

            return result;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}