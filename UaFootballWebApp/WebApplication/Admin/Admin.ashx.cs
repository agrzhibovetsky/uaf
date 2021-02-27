using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using UaFootball.AppCode;
using UaFDatabase;

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
            string response = "{\"result\": \"OK\"}";
            switch (action)
            {
                case "getLatestLineup":
                    {
                        response = GetLatestLineup(context.Request);
                        break;
                    }
                case "getNotesTypes":
                    {
                        response = GetNotesTypes();
                        break;
                    }
                case "deleteMatchNote":
                    {
                        int id = int.Parse(context.Request["id"]);
                        DeleteNote(id);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            context.Response.Write(response);
        }

        private void DeleteNote(int noteId)
        {
            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                MatchNote noteToDelete = db.MatchNotes.SingleOrDefault(mn => mn.MatchNote_Id == noteId);
                if (noteToDelete != null)
                {
                    db.MatchNotes.DeleteOnSubmit(noteToDelete);
                }
                db.SubmitChanges();
            }
        }

        private string GetNotesTypes()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(Constants.MatchNoteSetups);
        }

        private string GetLatestLineup(HttpRequest request)
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
                using (UaFootball_DBDataContext db = DBManager.GetDB())
                {
                    Match curMatch = db.Matches.Where(m => m.HomeClub_Id == clubId || m.AwayClub_Id == clubId).OrderByDescending(m => m.Date).FirstOrDefault();
                    Match latestMatch = db.Matches.Where(m => m.Match_Id!=curMatch.Match_Id && (m.HomeClub_Id == clubId || m.AwayClub_Id == clubId)).OrderByDescending(m => m.Date).FirstOrDefault();

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