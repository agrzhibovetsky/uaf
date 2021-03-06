﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using UaFootball.AppCode;

namespace UaFootball.WebApplication.Controls
{
    public partial class MatchLog_Player : UserControl
    {
        public object DataSource { get; set; }

        public int PlayerId { get; set; }

        public bool HideHeader { get; set; }

        public bool HideSeasonSeparator { get; set; }

        private int _curSeasonId = -1;

        protected override void  OnLoad(EventArgs e)
        {
            ClientScriptManager cs = Page.ClientScript;
            string startupScriptKey = "MatchLog_Player_Init";
            if (!cs.IsStartupScriptRegistered(startupScriptKey))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), startupScriptKey, "initMatchLog()", true);
            }
            
 	         base.OnLoad(e);
        }

        new public void DataBind()
        {
            if (DataSource != null)
            {
                rptMatchLog.DataSource = DataSource;
                rptMatchLog.DataBind();
            }
        }

        protected void rptMatches_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Header && HideHeader)
            {
                e.Item.Visible = false;
            }

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                MatchDTO match = e.Item.DataItem as MatchDTO;
                MatchLineupDTO lineup = match.Lineup[0];
                Label lblMinute = e.Item.FindControl("lblMinute") as Label;
                Label matchNo = e.Item.FindControl("lblMatchNo") as Label;
                bool seasonChanged = false;
                if (match.Season_Id != _curSeasonId)
                {
                    _curSeasonId = match.Season_Id;
                    seasonChanged = e.Item.ItemIndex > 0;
                }
                bool isFriendly = match.CompetitionStageName == null;

                string minuteStg = lineup.didntPlay ? string.Empty : lineup.minutesPlayed.ToString();

                if (lineup.cameAsSubstitute) minuteStg = "(" + minuteStg + ")";

                lblMinute.Text = minuteStg;
                if (!lineup.didntPlay) lblMinute.CssClass = "playerFullMatch";
                if (lineup.cameAsSubstitute) lblMinute.CssClass = "playerIn";
                if (lineup.wasSubstituted) lblMinute.CssClass = "playerOut";

                matchNo.Text = lineup.MatchNo > 0 ? lineup.MatchNo.ToString() : "";

                if ((lineup.IsHomeTeamPlayer && match.HomeTeamCountryCode != Constants.CountryCodeUA) || (!lineup.IsHomeTeamPlayer && match.AwayTeamCountryCode != Constants.CountryCodeUA))
                {
                    HtmlTableRow row = e.Item.FindControl("matchRow") as HtmlTableRow;
                    row.Style.Add(HtmlTextWriterStyle.Color, "#808080");
                }

                if (isFriendly)
                {
                    HtmlTableRow row = e.Item.FindControl("matchRow") as HtmlTableRow;
                    row.Style.Add(HtmlTextWriterStyle.Color, "#808080");
                }

                if (seasonChanged && !HideSeasonSeparator)
                {
                    HtmlTableRow row = e.Item.FindControl("matchRow") as HtmlTableRow;
                    row.Attributes["class"] += " new_competition";
                }
                //e.Item.

                Repeater rptEvents = e.Item.FindControl("rptEvents") as Repeater;
                rptEvents.ItemDataBound += rptEvents_ItemDataBound;
                rptEvents.DataSource = match.Events.Where(me => me.Event_Cd != Constants.DB.EventTypeCodes.Substitution);
                rptEvents.DataBind();

                HyperLink hlPhoto = e.Item.FindControl("hlPhoto") as HyperLink;

                if (match.PhotoCount > 0)
                {
                    hlPhoto.ToolTip = match.PhotoCount.ToString();
                    hlPhoto.NavigateUrl = ResolveClientUrl(string.Format("/UaFootball/WebApplication/Public/Photo.aspx?PlayerId={0}&MatchId={1}", PlayerId, match.Match_Id));
                }
                else
                {
                    hlPhoto.Visible = false;
                }
                
                Image iVideo = e.Item.FindControl("iVideo") as Image;
                iVideo.Visible = false;

            }
        }

        protected void rptEvents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            MatchEventDTO ev = e.Item.DataItem as MatchEventDTO;
            MatchEvent meControl = e.Item.FindControl("me") as MatchEvent;
            meControl.Player1 = ev.Player1;
            meControl.Player2 = ev.Player2;
        }
    }
}