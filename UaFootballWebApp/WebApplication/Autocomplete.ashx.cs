using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using UaFootball.AppCode;
using UaFDatabase;

namespace UaFootball.WebApplication
{
    
    public class Autocomplete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain"; 

            string searchTerm = context.Request["term"];
            string searchType = context.Request["type"];
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<AutoCompleteResponse> result = new List<AutoCompleteResponse>();

            AutocompleteType type = (AutocompleteType) Enum.Parse(typeof(AutocompleteType), searchType, true); 

            if (!searchTerm.IsEmpty() && !searchType.IsEmpty())
            {
                switch (type)
                {
                    default:
                        {
                            context.Response.Write(string.Empty);
                            break;
                        }
                    case AutocompleteType.Logo:  
                    case AutocompleteType.PlayerPhoto:
                        {
                            DirectoryInfo logosDirInfo = new DirectoryInfo(Constants.Paths.DropBoxPath);
                            if (logosDirInfo.Exists)
                            {
                                FileInfo[] files = logosDirInfo.GetFiles(searchTerm + "*");
                                result.AddRange(files.Select(f => new AutoCompleteResponse { id = 1, value = f.Name }));
                                context.Response.Write(serializer.Serialize(result));
                            }
                            else
                            {
                                context.Response.Write(string.Empty);
                            }

                            break;
                        }
                    case AutocompleteType.Club:
                        {
                            using (UaFootball_DBDataContext db = DBManager.GetDB())
                            {
                                IQueryable<AutoCompleteResponse> searchMatches = db.Clubs.Where(c => c.Club_Name.Contains(searchTerm)).Select(c => new AutoCompleteResponse { id = c.Club_ID, value = string.Concat(c.Club_Name, " (", c.City.City_Name, ")") });
                                result.AddRange(searchMatches);
                                context.Response.Write(serializer.Serialize(result));
                            }
                            break;
                        }
                    case AutocompleteType.NationalTeam:
                        {
                            using (UaFootball_DBDataContext db = DBManager.GetDB())
                            {
                                var searchMatches =
                                    from natTeam in db.NationalTeams
                                    join country in db.Countries on natTeam.Country_Id equals country.Country_ID
                                    where country.Country_Name.Contains(searchTerm) && natTeam.NationalTeamType_Cd=="NAT"
                                    select new { id = natTeam.NationalTeam_Id, value = country.Country_Name, code = natTeam.NationalTeamType_Cd };

                                foreach (var item in searchMatches)
                                {
                                    result.Add(new AutoCompleteResponse { id = item.id, value = item.value + " " + UIHelper.FormatNationalTeamType(item.code) });
                                }

                                context.Response.Write(serializer.Serialize(result));
                            }
                            break;
                        }
                    case AutocompleteType.Referee:
                        {
                            using (UaFootball_DBDataContext db = DBManager.GetDB())
                            {
                                IQueryable<AutoCompleteResponse> searchMatches = db.Referees.Where(r => r.LastName.Contains(searchTerm)).Select(r => new AutoCompleteResponse { id = r.Referee_Id, value = r.FirstName + " " + r.LastName });
                                result.AddRange(searchMatches);
                                context.Response.Write(serializer.Serialize(result));
                            }
                            break;
                        }
                    case AutocompleteType.Coach:
                        {
                            using (UaFootball_DBDataContext db = DBManager.GetDB())
                            {
                                IQueryable<AutoCompleteResponse> searchMatches = db.Coaches.Where(r => r.LastName.Contains(searchTerm)).Select(r => new AutoCompleteResponse { id = r.CoachId, value = r.FirstName + " " + r.LastName });
                                result.AddRange(searchMatches);
                                context.Response.Write(serializer.Serialize(result));
                            }
                            break;
                        }
                    case AutocompleteType.Player:
                        {
                            using (UaFootball_DBDataContext db = DBManager.GetDB())
                            {
                                List<UaFDatabase.Player> searchMatches = db.Players.Where(r => r.Last_Name.StartsWith(searchTerm) || r.Display_Name.StartsWith(searchTerm) || r.Last_Name_Int.StartsWith(searchTerm)).ToList();
                                if (!string.IsNullOrEmpty(context.Request["nationalTeam"]))
                                {
                                    int nationalTeamId = int.Parse(context.Request["nationalTeam"]);
                                    int countryId = db.NationalTeams.Single(nt => nt.NationalTeam_Id == nationalTeamId).Country_Id;
                                    searchMatches = searchMatches.Where(m => m.Country_Id == countryId).ToList();
                                }
                                foreach (UaFDatabase.Player p in searchMatches)
                                {
                                    AutoCompleteResponse r = new AutoCompleteResponse { id = p.Player_Id };
                                    string formattedName = string.Format("{0} {1} '{2}'", p.First_Name, p.Last_Name, p.Display_Name);
                                    formattedName = formattedName.Replace("''", "").Trim();
                                    r.value = formattedName;
                                    result.Add(r);
                                }
                                context.Response.Write(serializer.Serialize(result));
                            }
                            break;
                        }
                    case AutocompleteType.SearchPlayerByShirtNum:
                        {
                            string[] parameters = searchTerm.Split(';');
                            if (parameters.Length == 4)
                            {
                                int teamId = int.Parse(parameters[0]);
                                int shirtNum = int.Parse(parameters[1]);
                                bool isNationalTeamMatch = bool.Parse(parameters[2]);
                                bool searchBackward = bool.Parse(parameters[3]);
                                using (UaFootball_DBDataContext db = DBManager.GetDB())
                                {
                                    IQueryable<MatchLineup> dbLineups = null;

                                    if (!isNationalTeamMatch)
                                    {
                                        dbLineups = (from lineup in db.MatchLineups
                                             where lineup.ShirtNumber == shirtNum && (lineup.IsHomeTeamPlayer && lineup.Match.HomeClub_Id == teamId || !lineup.IsHomeTeamPlayer && lineup.Match.AwayClub_Id == teamId)
                                             //orderby lineup.Match.Date descending 
                                             select lineup);//.FirstOrDefault();
                                              
                                    }
                                    else
                                    {
                                        dbLineups = (from lineup in db.MatchLineups
                                                     where lineup.ShirtNumber == shirtNum && (lineup.IsHomeTeamPlayer && lineup.Match.HomeNationalTeam_Id == teamId || !lineup.IsHomeTeamPlayer && lineup.Match.AwayNationalTeam_Id == teamId)
                                                     //orderby lineup.Match.Date descending
                                                     select lineup);//.FirstOrDefault();
                                    }

                                    if (searchBackward)
                                        dbLineups = dbLineups.OrderBy(l=>l.Match.Date);
                                    else
                                        dbLineups = dbLineups.OrderByDescending(l=>l.Match.Date);

                                    MatchLineup lu = dbLineups.FirstOrDefault();
                                    AutoCompleteResponse resp = null;
                                    if (lu != null)
                                    {
                                        UaFDatabase.Player p = lu.Player;
                                        string formattedName = string.Empty;
                                        if (!string.IsNullOrEmpty(p.Display_Name))
                                        {
                                            formattedName = string.Format("{0} ({1} {2})", p.Display_Name.ToUpper(), p.First_Name, p.Last_Name);
                                        }
                                        else
                                        {
                                            formattedName = string.Format("{0} {1}", p.First_Name, p.Last_Name);
                                        }

                                        if ((lu.Flags & Constants.DB.LineupFlags.Goalkeeper) > 0)
                                            formattedName = "*" + formattedName;
                                        resp = new AutoCompleteResponse { id = p.Player_Id, value = formattedName };
                                    }
                                    else
                                    {
                                        resp = new AutoCompleteResponse();
                                    }
                                    context.Response.Write(serializer.Serialize(resp));
                                }
                            }
                            break;
                        }
                    case AutocompleteType.MostRecentClubCoach:
                        {
                            //term: [nc]Id
                            bool isNational = searchTerm[0] == 'n';
                            int teamId = int.Parse(searchTerm.Substring(1));
                            AutoCompleteResponse resp = new AutoCompleteResponse();
                            using (UaFootball_DBDataContext db = DBManager.GetDB())
                            {
                                Match mostRecentMatch = (from match in db.Matches
                                              where (match.HomeClub_Id == teamId || match.AwayClub_Id == teamId) && !isNational || (match.HomeNationalTeam_Id == teamId || match.AwayNationalTeam_Id == teamId) && isNational
                                              orderby match.Date descending
                                              select match).FirstOrDefault();
                                if (mostRecentMatch != null)
                                {
                                    bool isHomeTeam = mostRecentMatch.HomeClub_Id == teamId || mostRecentMatch.HomeNationalTeam_Id == teamId;
                                    Coach teamCoach = (from ml in db.MatchLineups
                                                       where (ml.Match_Id == mostRecentMatch.Match_Id) && (ml.IsHomeTeamPlayer == isHomeTeam) && (ml.Coach_Id > 0)
                                                       select ml.Coach).FirstOrDefault();
                                    if (teamCoach != null)
                                    {
                                        resp.id = teamCoach.CoachId;
                                        resp.value = teamCoach.FirstName + " " + teamCoach.LastName;
                                    }
                                }
                                context.Response.Write(serializer.Serialize(resp));
                                
                            }
                            break;
                        }
                    case AutocompleteType.MostRecentStadium:
                        {
                            using (UaFootball_DBDataContext db = DBManager.GetDB())
                            {
                                //term: [nc]Id
                                bool isNational = searchTerm[0] == 'n';
                                int teamId = int.Parse(searchTerm.Substring(1));
                                AutoCompleteResponse resp = new AutoCompleteResponse();

                                Match mostRecentMatch = (from match in db.Matches
                                                         where (match.HomeClub_Id == teamId) && !isNational || (match.HomeNationalTeam_Id == teamId) && isNational
                                                         orderby match.Date descending
                                                         select match).FirstOrDefault();

                                if (mostRecentMatch != null)
                                {
                                    resp.id = mostRecentMatch.Stadium_Id;
                                    resp.value = "";
                                }
                                context.Response.Write(serializer.Serialize(resp));

                            }
                            break;
                        }
                    case AutocompleteType.EventPlayer:
                        string response = String.Empty;
                        using (UaFootball_DBDataContext db = DBManager.GetDB())
                        {
                            string eventIdParam = context.Request["eventId"];
                            if (!string.IsNullOrEmpty(eventIdParam))
                            {
                                int eventId = int.Parse(eventIdParam);
                                UaFDatabase.MatchEvent me = db.MatchEvents.Single(m => m.MatchEvent_Id == eventId);
                                List<MatchLineup> lineups = db.MatchLineups.Where(ml => ml.Match_Id == me.Match_Id).ToList();
                                bool isHomeTeamPlayer = lineups.Single(l => l.Player_Id == me.Player1_Id).IsHomeTeamPlayer;
                                List<UaFDatabase.Player> sameTeamPlayers = db.Players.Where(p => lineups.Where(l => l.IsHomeTeamPlayer == isHomeTeamPlayer).Select(l => l.Player_Id).Contains(p.Player_Id)).ToList();
                                foreach (UaFDatabase.Player p in sameTeamPlayers)
                                {
                                    if (p.Last_Name.ToLower().StartsWith(searchTerm.ToLower()) || (p.Display_Name??string.Empty).ToLower().StartsWith(searchTerm.ToLower()))
                                    {
                                        AutoCompleteResponse r = new AutoCompleteResponse { id = p.Player_Id };
                                        string formattedName = string.Format("{0} {1} '{2}'", p.First_Name, p.Last_Name, p.Display_Name);
                                        formattedName = formattedName.Replace("''", "").Trim();
                                        r.value = formattedName;
                                        result.Add(r);
                                    }
                                }
                                response = serializer.Serialize(result);
                            }
                        }
                        context.Response.Write(response);
                        break;
                }
            }
            else
            {
                context.Response.Write(string.Empty);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class AutoCompleteResponse
    {
        public string value { get; set; }
        public int id { get; set; }
    } 
}