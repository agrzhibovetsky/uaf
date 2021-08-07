using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using UaFootball.AppCode;
using UaFDatabase;

namespace UaFootball.WebApplication
{
    public partial class Game : UaFootballPageBase
    {
        protected MatchDTO DataItem
        {
            get
            {
                return ViewState["DataItem"] as MatchDTO;
            }
            set
            {
                ViewState["DataItem"] = value;
            }
        }

        protected string GetTeamUrl(int? clubId, int? nationalTeamId)
        {
            string pageUrl = clubId.HasValue ? "Club.aspx" : "NationalTeam.aspx";
            int teamId = clubId.HasValue ? clubId.Value : nationalTeamId.Value;
            return pageUrl + "?id=" + teamId;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptInclude(Constants.Paths.ColorboxKey, Page.ResolveClientUrl(Constants.Paths.ColorboxPath));
            if (!IsPostBack)
            {
                if (Request[Constants.QueryParam.ObjectId] != null)
                {
                    int objectId = int.Parse(Request[Constants.QueryParam.ObjectId]);
                    DataItem = new MatchDTOHelper().GetFromDB(objectId);

                    string thumbPath = "\\thumb\\";
                    if (DataItem.HomeTeamLogo != null)
                    {
                        iHomeTeamLogo.ImageUrl = PathHelper.GetWebPath(this, Constants.Paths.MutlimediaWebRoot, DataItem.HomeTeamLogo.FilePath + thumbPath, DataItem.HomeTeamLogo.FileName);
                    }
                    if (DataItem.AwayTeamLogo != null)
                    {
                        iAwayTeamLogo.ImageUrl = PathHelper.GetWebPath(this, Constants.Paths.MutlimediaWebRoot, DataItem.AwayTeamLogo.FilePath + thumbPath, DataItem.AwayTeamLogo.FileName);
                    }

                    if ((DataItem.Flags & Constants.DB.MatchFlags.GoldenGoalRule)>0)
                    {
                        DataItem.SpecialNote += "Матч закончился в дополнительное время по правилу золотого гола";
                    }
                    /*lblSpecialNotes.Text = DataItem.SpecialNote;
                    lblSpecialNotes.Visible = !DataItem.SpecialNote.IsEmpty();*/

                    int imageCount = DataItem.Multimedia.Count(m => m.MultimediaType_CD == Constants.DB.MutlimediaTypes.Image);
                    int videoCount = DataItem.Multimedia.Count(m => m.MultimediaType_CD == Constants.DB.MutlimediaTypes.Video);
                    hlPhoto.Text = "Фото (" + imageCount.ToString() + ")";
                    hlVideo.Text = "Видео (" + videoCount.ToString() + ")";
                    hlPhoto.NavigateUrl = ResolveClientUrl("~/WebApplication/Public/Photo.aspx?MatchId=") + DataItem.Match_Id.ToString();
                    hlVideo.NavigateUrl = ResolveClientUrl("~/WebApplication/Public/Video.aspx?MatchId=") + DataItem.Match_Id.ToString();

                    List<MatchLineupDTO> homePlayers = DataItem.Lineup.Where(l => l.IsHomeTeamPlayer && l.CoachId == null).ToList();
                    List<MatchLineupDTO> awayPlayers = DataItem.Lineup.Where(l => !l.IsHomeTeamPlayer && l.CoachId == null).ToList();
                    MatchLineupDTO homeCoachDTO = DataItem.Lineup.FirstOrDefault(l => l.CoachId.HasValue && l.IsHomeTeamPlayer);
                    MatchLineupDTO awayCoachDTO = DataItem.Lineup.FirstOrDefault(l => l.CoachId.HasValue && !l.IsHomeTeamPlayer);
                    if (homeCoachDTO != null)
                    {
                        hlHomeTeamCoach.Text = string.Concat(homeCoachDTO.Coach_FirstName, " ", homeCoachDTO.Coach_LastName);
                        hlHomeTeamCoach.NavigateUrl = ResolveClientUrl("~/WebApplication/Public/Coach.aspx?objectId=" + homeCoachDTO.CoachId);
                        ltHomeCoachInCharge.Visible = (homeCoachDTO.Flags & Constants.DB.LineupFlags.CoachInCharge) > 0;
                    }
                    if (awayCoachDTO != null)
                    {
                        hlAwayTeamCoach.Text = string.Concat(awayCoachDTO.Coach_FirstName, " ", awayCoachDTO.Coach_LastName);
                        hlAwayTeamCoach.NavigateUrl = ResolveClientUrl("~/WebApplication/Public/Coach.aspx?objectId=" + awayCoachDTO.CoachId);
                        ltAwayCoachInCharge.Visible = (awayCoachDTO.Flags & Constants.DB.LineupFlags.CoachInCharge) > 0;
                    }

                    int rowsCount = Math.Max(homePlayers.Count, awayPlayers.Count);
                    List<Pair> uiLineup = new List<Pair>(rowsCount);
                    Pair a = new Pair();
                    for (int i = 0; i < rowsCount; i++)
                    {
                        Pair uiLineupPair = new Pair();

                        if (homePlayers.Count > i)
                        {
                            uiLineupPair.First = homePlayers[i];
                        }
                        else
                        {
                            uiLineupPair.First = new MatchLineupDTO();
                        }

                        if (awayPlayers.Count > i)
                        {
                            uiLineupPair.Second = awayPlayers[i];
                        }
                        else
                        {
                            uiLineupPair.Second = new MatchLineupDTO();
                        }

                        uiLineup.Add(uiLineupPair);
                    }


                    rptLineup.DataSource = uiLineup;
                    rptLineup.DataBind();

                    rptEvents.DataSource = DataItem.Events.Where(ev=>ev.Event_Cd != Constants.DB.EventTypeCodes.Substitution).OrderBy(ev=>ev.Minute);
                    rptEvents.DataBind();

                    rptNotes.DataSource = DataItem.Notes;
                    rptNotes.DataBind();

                    Controls.MatchNotes[] matchNotes = { mnSpect, mnAwayLineup, mnHomeLineup, mnAwayCoach, mnNoSpect, mnHomeCoach };
                    foreach (Controls.MatchNotes mn in matchNotes)
                    {
                        mn.DataSource = DataItem.Notes;
                        mn.DataBind();
                    }
                    
                    lblStadiumDisq.Visible = (DataItem.Flags.HasValue && ((DataItem.Flags & Constants.DB.MatchFlags.StadiumDisqualifiedNoSpectators) > 0));
                    lblNeutralField.Visible = (DataItem.Flags.HasValue && ((DataItem.Flags & Constants.DB.MatchFlags.NeutralField) > 0));
                    
                }

            }
        }

        protected void FormatPlayerHyperLink(HyperLink h, MatchLineupDTO pl)
        {
            h.Text = FormatName(pl.Player_FirstName, pl.Player_LastName, pl.Player_DisplayName, pl.Player_CountryId);
            if ((pl.Flags & Constants.DB.LineupFlags.Goalkeeper) > 0)
            {
                h.Text += " (Вр) ";
            }
            if ((pl.Flags & Constants.DB.LineupFlags.Captain) > 0)
            {
                h.Text += " (K) ";
            }
            if ((pl.Flags & Constants.DB.LineupFlags.Debut) > 0)
            {
                h.Text += " (Дебют) ";
            }
            h.ToolTip = FormatName(pl.Player_FirstName_Int, pl.Player_LastName_Int, null, pl.Player_CountryId);
            h.NavigateUrl = string.Format("Player.aspx?{0}={1}", Constants.QueryParam.PlayerId, pl.Player_Id);
        }

        protected void FormatPlayerSubstHyperLink(HyperLink h, MatchEventDTO me)
        {
            h.Text = FormatName(me.Player2.First_Name, me.Player2.Last_Name, me.Player2.Display_Name, me.Player2.Country_Id) + ", " + me.Minute.ToString();
            h.NavigateUrl = string.Format("Player.aspx?{0}={1}", Constants.QueryParam.PlayerId, me.Player2_Id);
        }
        protected void rptLineup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Pair l = e.Item.DataItem as Pair;
            MatchLineupDTO hp = l.First as MatchLineupDTO;
            MatchLineupDTO ap = l.Second as MatchLineupDTO;

            Label lblHPlayerNum = e.Item.FindControl("lblHPlayerNum") as Label;
            lblHPlayerNum.Text = hp.ShirtNum.HasValue & hp.ShirtNum>0 ? hp.ShirtNum.Value.ToString() : string.Empty;

            Label lblAPlayerNum = e.Item.FindControl("lblAPlayerNum") as Label;
            lblAPlayerNum.Text = ap.ShirtNum.HasValue & ap.ShirtNum>0 ? ap.ShirtNum.Value.ToString() : string.Empty;

            Panel pHomePlayer = e.Item.FindControl("pHomePlayer") as Panel;
            HyperLink aHomePlayer = pHomePlayer.FindControl("aHomePlayer") as HyperLink;
            FormatPlayerHyperLink(aHomePlayer, hp);

            MatchEventDTO hPlayerSubstEvent =  DataItem.Events.FirstOrDefault(ev=> ev.Player1_Id == hp.Player_Id && ev.Event_Cd == Constants.DB.EventTypeCodes.Substitution);
            if (hPlayerSubstEvent != null)
            {
                Panel pHomePlayerSubs = e.Item.FindControl("pHomePlayerSubst") as Panel;
                pHomePlayerSubs.Visible = true;
                HyperLink aHomePlayerSubst = pHomePlayerSubs.FindControl("aHomePlayerSubst") as HyperLink;
                FormatPlayerSubstHyperLink(aHomePlayerSubst, hPlayerSubstEvent);
            }

            Panel pAwayPlayer = e.Item.FindControl("pAwayPlayer") as Panel;
            HyperLink aAwayPlayer = pHomePlayer.FindControl("aAwayPlayer") as HyperLink;
            FormatPlayerHyperLink(aAwayPlayer, ap);

            MatchEventDTO aPlayerSubstEvent = DataItem.Events.FirstOrDefault(ev => ev.Player1_Id == ap.Player_Id && ev.Event_Cd == Constants.DB.EventTypeCodes.Substitution);
            if (aPlayerSubstEvent != null)
            {
                Panel pAwayPlayerSubs = e.Item.FindControl("pAwayPlayerSubst") as Panel;
                pAwayPlayerSubs.Visible = true;
                HyperLink aAwayPlayerSubst = pAwayPlayerSubs.FindControl("aAwayPlayerSubst") as HyperLink;
                FormatPlayerSubstHyperLink(aAwayPlayerSubst, aPlayerSubstEvent);
            }

            if (e.Item.ItemIndex == 10)
            {
                HtmlTableRow trRow = e.Item.FindControl("trPlayerRow") as HtmlTableRow;
                trRow.Attributes.Add("class", "trSub2");
            }

        }

        protected void rptEvents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            MatchEventDTO ev = e.Item.DataItem as MatchEventDTO;
            MatchLineupDTO lineup;
            if (ev.Event_Cd == Constants.DB.EventTypeCodes.CoachYellowCard)
            {
                lineup = DataItem.Lineup.FirstOrDefault(l => l.CoachId == ev.Coach_Id);
            }
            else
            {
                lineup = DataItem.Lineup.FirstOrDefault(l => l.Player_Id == ev.Player1_Id);
            }
            if (lineup != null)
            {
                MatchEvent meControl = null;
                Label lblPlayerName = null;

                bool isOwnGoal = ev.Event_Cd == Constants.DB.EventTypeCodes.Goal && ((ev.EventFlags & Constants.DB.EventFlags.OwnGoal) > 0);

                if ((lineup.IsHomeTeamPlayer && !isOwnGoal) || (!lineup.IsHomeTeamPlayer && isOwnGoal))
                {
                    meControl = e.Item.FindControl("meHome") as MatchEvent;
                    lblPlayerName = e.Item.FindControl("lblHomeEvent") as Label;
                }
                else
                {
                    meControl = e.Item.FindControl("meAway") as MatchEvent;
                    lblPlayerName = e.Item.FindControl("lblAwayEvent") as Label;
                }

                meControl.Visible = true;
                meControl.EventType_CD = ev.Event_Cd;
                meControl.Minute = ev.Minute;
                meControl.EventFlags = ev.EventFlags;
                meControl.Player1 = ev.Player1;
                meControl.Player2 = ev.Player2;
                if (ev.Event_Cd == Constants.DB.EventTypeCodes.CoachYellowCard)
                {
                    lblPlayerName.Text = FormatName(lineup.Coach_FirstName, lineup.Coach_LastName, null, null) + " (тр), " + ev.Minute.ToString();
                }
                else
                {
                    lblPlayerName.Text = FormatName(lineup.Player_FirstName, lineup.Player_LastName, lineup.Player_DisplayName, lineup.Player_CountryId) + ", " + ev.Minute.ToString();
                }
            }
        }
    }
}