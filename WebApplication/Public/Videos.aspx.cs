using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;
using UaFDatabase;


namespace UaFootball.WebApplication.Public
{
    public partial class Videos: UaFootballPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                IQueryable<MatchEvent> events = null;
                if (playerId > 0)
                {
                    //var mm = db.MultimediaTags.Where(mt=>mt.Multimedia.MultimediaType_CD == Constants.DB.MutlimediaTypes.Video && (mt.MatchEvent.Player1_Id == playerId || mt.MatchEvent.Player2_Id == playerId));
                    var mm = (from multimediaTag in db.MultimediaTags
                             join multimedia in db.Multimedias on multimediaTag.Multimedia_ID equals multimedia.Multimedia_ID
                             join matchEvent in db.MatchEvents on multimediaTag.MatchEvent_ID equals matchEvent.MatchEvent_Id
                             join vMatch in db.vwMatches on matchEvent.Match_Id equals vMatch.Match_ID
                             where matchEvent.Player1_Id==playerId || matchEvent.Player2_Id == playerId
                             orderby vMatch.Date descending
                             select new 
                             {
                                 HomeTeam = vMatch.HomeTeam,
                                 AwayTeam = vMatch.AwayTeam,
                                 VideoType = matchEvent.Event_Cd == Constants.DB.EventTypeCodes.Penalty ? "Пенальти" : matchEvent.Player1_Id == playerId ? "Гол" : "Пас",
                                 Minute = matchEvent.Minute,
                                 EventFlags = matchEvent.EventFlags,
                                 File = multimedia,
                                 EventTypeCode = matchEvent.Event_Cd,
                                 MatchDate = vMatch.Date,
                                 MatchType = vMatch.CompetitionLevel_Cd
                             }).ToList();

                    
                    foreach (var m in mm)
                    {
                        
                    }
                    

                    rptVideos.DataSource = mm;
                    rptVideos.DataBind();
                }
            }
        }

        protected string FormatEventFlags(string eventTypeCode, long eventFlags, string videoType, int subType)
        {
            Dictionary<int, string> eventFlagMap = UIHelper.EventCodeEventFlagsMap[eventTypeCode];
            var descr = "";
            if (videoType == "Гол" || videoType=="Пенальти")
            {

                foreach (int flag in eventFlagMap.Keys)
                {
                    if ((flag & eventFlags) > 0)
                    {
                        bool toAdd = false;
                        int type = 4;
                        if (videoType == "Гол")
                        {
                            if (flag == Constants.DB.EventFlags.LeftLeg || flag == Constants.DB.EventFlags.RightLeg || flag == Constants.DB.EventFlags.Head || flag == Constants.DB.EventFlags.OtherBodyPart)
                                type = 1;


                            if (flag == Constants.DB.EventFlags.GoalClass1 || flag == Constants.DB.EventFlags.GoalClass2 || flag == Constants.DB.EventFlags.GoalClass3 || flag == Constants.DB.EventFlags.GoalClass4)
                                type = 2;

                            if (flag == Constants.DB.EventFlags.LongDistance || flag == Constants.DB.EventFlags.ShortDistance || flag == Constants.DB.EventFlags.MiddleDistance)
                                type = 3;
                        }
                        else
                        {
                            if (flag == Constants.DB.EventFlags.LeftLeg || flag == Constants.DB.EventFlags.RightLeg || flag == Constants.DB.EventFlags.Head || flag == Constants.DB.EventFlags.OtherBodyPart)
                                type = 1;

                            if (flag == Constants.DB.EventFlags.LeftBottom || flag == Constants.DB.EventFlags.RightBottom || flag == Constants.DB.EventFlags.LeftTop || flag == Constants.DB.EventFlags.RightTop)
                                type = 4;

                        }
                        if (type == subType)
                        {
                            descr += ", " + eventFlagMap[flag];
                        }

                    }
                }

                if (descr.Length > 0)
                {
                    descr = descr.Substring(1);
                }
            }
            return descr;
        }
    }
}