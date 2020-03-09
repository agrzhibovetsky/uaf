using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;
using UaFootball.DB;

namespace UaFootball.WebApplication.Public
{
    public partial class Photo : System.Web.UI.Page
    {
        protected class jssorData
        {
            public string filePath {get; set;}
            public string fileThumbPath {get; set;}
            public DateTime gameDate { get; set; }
            public string source { get; set; }
            public string author { get; set; }
            public string comments { get; set; }
            public string playersStg { get; set; }
            public string homeTeam { get; set; }
            public string awayTeam { get; set; }
            public int Id { get; set; }
            public string title1 
            {
                get
                {
                    return string.Format("{0}. {1}-{2}. {3}", UIHelper.FormatDate(gameDate), homeTeam, awayTeam, playersStg);
                }
            }
            public string title2 
            {
                get
                {
                    string s1 = comments.IsEmpty() ? string.Empty : comments + ". ";
                    string authorSeparator = source.IsEmpty() ? "." : ", ";
                    string s2 = author.IsEmpty() ? string.Empty : author + authorSeparator;

                    string s4 = !author.IsEmpty() || !source.IsEmpty() ? "Фото: " : string.Empty;
                    return string.Concat(s1, s4, s2, source);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptInclude(Constants.Paths.JQueryKey, Page.ResolveClientUrl(Constants.Paths.JQueryPath));

            int matchId = 0;
            int playerId = 0;
            if (Request["MatchId"] != null)
            {
                matchId = int.Parse(Request["MatchId"]);
            }
            if (Request["PlayerId"] != null)
            {
                playerId = int.Parse(Request["PlayerId"]);
            }
            List<jssorData> data = new List<jssorData>();
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {

                var multimedias = from m in db.Multimedias
                                     join match_tag in db.MultimediaTags on m.Multimedia_ID equals match_tag.Multimedia_ID
                                     join filter_tag in db.MultimediaTags on m.Multimedia_ID equals filter_tag.Multimedia_ID
                                     where match_tag.Match_ID != null
                                     select new {match_tag = match_tag, filter_tag = filter_tag, m = m};

                
                if (matchId > 0)
                {
                    multimedias = multimedias.Where(m => m.match_tag.Match_ID == matchId);
                }
                if (playerId > 0)
                {
                    multimedias = multimedias.Where(m => m.filter_tag.Player_ID == playerId);
                }

                var tags = from multimedia in multimedias
                           join g in db.vwMatches on multimedia.match_tag.Match_ID equals g.Match_ID
                           join tag in db.MultimediaTags on multimedia.m.Multimedia_ID equals tag.Multimedia_ID
                           join p in db.Players on tag.Player_ID equals p.Player_Id into pg
                           from p2 in pg.DefaultIfEmpty()
                           where multimedia.m.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.MatchPhoto
                           select new { Multimedia = multimedia.m, Match = g, Player = p2 };
               
                foreach (int multimediaId in tags.Select(t=>t.Multimedia.Multimedia_ID).Distinct())
                {
                    Multimedia m = tags.First(t => t.Multimedia.Multimedia_ID == multimediaId).Multimedia;
                    vwMatch game = tags.First(t => t.Multimedia.Multimedia_ID == multimediaId).Match;
                    List<DB.Player> players = tags.Where(t => t.Multimedia.Multimedia_ID == multimediaId && t.Player != null).Select(t => t.Player).Distinct().ToList();
                    jssorData d = new jssorData()
                    {
                        filePath = PathHelper.GetWebPath(this, Constants.Paths.MutlimediaWebRoot, m.FilePath, m.FileName),
                        fileThumbPath = PathHelper.GetWebPath(this, Constants.Paths.MutlimediaWebRoot, m.FilePath.Trim() + "/thumb/", m.FileName),
                        author = m.Author,
                        comments = m.Description,
                        source = m.Source,
                        gameDate = game.Date,
                        playersStg = string.Join(", ", players.Select(p=>UIHelper.FormatName(p.First_Name, p.Last_Name, p.Display_Name, p.Country_Id))),
                        homeTeam = game.HomeTeam,
                        awayTeam = game.AwayTeam,
                        Id=m.Multimedia_ID
                    };
                    
                    data.Add(d);
                }
            }
            rptJssorContent.DataSource = data.OrderByDescending(d=>d.gameDate);
            rptJssorContent.DataBind();
        }
    }
}