using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;

namespace UaFootball.WebApplication
{
    public partial class MatchEvent : UserControl
    {
        public string EventType_CD { get; set; }

        public int Minute { get; set; }

        public long? EventFlags { get; set; }

        public bool AppliesToSecondPlayer { get; set; }

        public PlayerDTO Player1 { get; set; }

        public PlayerDTO Player2 { get; set; }

        public bool HasVideo { get; set; }

        public int EventId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (EventType_CD != null)
            {
                iEvent.ToolTip = Minute.ToString() + "'";
                Dictionary<int, string> eventFlagMap = UIHelper.EventCodeEventFlagsMap[EventType_CD];

                foreach (int flag in eventFlagMap.Keys)
                {
                    if ((flag & EventFlags) > 0)
                    {
                        iEvent.ToolTip += ", " + eventFlagMap[flag];
                    }
                }

                switch (EventType_CD)
                {
                    case Constants.DB.EventTypeCodes.Goal:
                        {
                            if (AppliesToSecondPlayer)
                            {
                                iEvent.ImageUrl = ResolveClientUrl("~/WebApplication/images/assist2.png");
                                iEvent.ToolTip = "Гол: " + UIHelper.FormatName(Player1) +" - " + iEvent.ToolTip;
                            }
                            else
                            {
                                if (Player2 != null)
                                {
                                    iEvent.ToolTip = "Пас: " + UIHelper.FormatName(Player2) + " - " + iEvent.ToolTip;
                                }
                                if ((EventFlags & Constants.DB.EventFlags.OwnGoal) > 0)
                                    iEvent.ImageUrl = ResolveClientUrl("~/WebApplication/images/own_goal.png");
                                else
                                    iEvent.ImageUrl = ResolveClientUrl("~/WebApplication/images/ball.gif");
                            }
                            break;
                        }
                    case Constants.DB.EventTypeCodes.Penalty:
                        {
                            iEvent.ImageUrl = ResolveClientUrl("~/WebApplication/images/penalty.png");
                            if (EventFlags.HasValue && ((EventFlags.Value & Constants.DB.EventFlags.PostMatchPenalty) > 0))
                            {
                                iEvent.Style.Add("opacity", "0.3");
                            }
                            break;
                        }
                    case Constants.DB.EventTypeCodes.YellowCard:
                        {
                            iEvent.ImageUrl = ResolveClientUrl("~/WebApplication/images/yells.gif");
                            break;
                        }
                    case Constants.DB.EventTypeCodes.RedCard:
                        {
                            iEvent.ImageUrl = ResolveClientUrl("~/WebApplication/images/reds.gif"); break;
                        }
                    case Constants.DB.EventTypeCodes.SecondYellowCard:
                        {
                            iEvent.ImageUrl = ResolveClientUrl("~/WebApplication/images/yellow2.gif"); break;
                        }
                    case Constants.DB.EventTypeCodes.MissedPenalty:
                        {
                            iEvent.ImageUrl = ResolveClientUrl("~/WebApplication/images/nogoal.png");
                            if (EventFlags.HasValue && ((EventFlags.Value & Constants.DB.EventFlags.PostMatchPenalty) > 0))
                            {
                                iEvent.Style.Add("opacity", "0.3");
                            }
                            break;
                        }
                    default:
                        {
                            iEvent.Visible = false; break;
                        }
                }
            }

            if (HasVideo)
            {
                iEvent.Attributes.Add("onclick", string.Format("open_colorbox_video(event, '{0}')", PathHelper.GetWebPath(this.Page, Constants.Paths.RootWebPath, "Public", "Video.aspx?EventId=" + EventId.ToString())));
                iEvent.Attributes.Add("onclick", string.Format("open_colorbox_video(event, '{0}')", PathHelper.GetWebPath(this.Page, Constants.Paths.RootWebPath, "Public", "Video.aspx?EventId=" + EventId.ToString())));
                iEvent.Style.Add("background-color", "#E5F5DF");
            }
        }
    }
}