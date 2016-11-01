using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using UaFootball.AppCode;
using UaFootball.DB;

namespace UaFootball.WebApplication
{
    public partial class MatchEdit : ObjectEditPageBase<MatchDTO>
    {
        protected override string ObjectListPage
        {
            get { return Constants.Pages.List_Match; }
        }

        protected override void OnLoad(EventArgs e)
        {
            rfvCompetitions.InitialValue = Constants.UI.DropdownDefaultValue;
            rfvSeasons.InitialValue = Constants.UI.DropdownDefaultValue;
            rfvStadiums.InitialValue = Constants.UI.DropdownDefaultValue;
            

            SetAutocompleteTypes();
            
            base.OnLoad(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptInclude(Constants.Paths.JQueryKey, Page.ResolveClientUrl(Constants.Paths.JQueryPath));
            Page.ClientScript.RegisterClientScriptInclude(Constants.Paths.JQueryUIKey, Page.ResolveClientUrl(Constants.Paths.JQueryUIPath));
        }

        protected override void PrepareUI()
        {
            bool isNationalTeamMatch = DataItem.HomeNationalTeam_Id.HasValue;
            string competitionFilter = isNationalTeamMatch ? Constants.DB.CompetitionLevelCd_NationalTeam : Constants.DB.CompetitionLevelCd_Club;

            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                BindDropdown(db, Constants.ObjectType.Competition, ddlCompetitions, true, competitionFilter);
                BindDropdown(db, Constants.ObjectType.Season, ddlSeasons, true, competitionFilter);
                BindDropdown(db, Constants.ObjectType.Stadium, ddlStadiums, true, string.Empty);
            }

            if (isNewObject)
            {
                tbDate.Text = FormatDate(DateTime.Now);
            }

            cblMatchFlags.DataSource = UIHelper.MatchFlagsMap;
            cblMatchFlags.DataTextField = "Value"; ;
            cblMatchFlags.DataValueField = "Key";
            cblMatchFlags.DataBind();
            
        }

        protected override void DTOToUI(MatchDTO dtoObj)
        {
            bool isNationalTeamMatch = dtoObj.HomeNationalTeam_Id.HasValue;
            cbMatchKind.Checked = isNationalTeamMatch;
            actbHomeTeam.Text = dtoObj.HomeTeamName;
            actbAwayTeam.Text = dtoObj.AwayTeamName;
            
            if (isNationalTeamMatch)
            {
                actbHomeTeam.Value = dtoObj.HomeNationalTeam_Id.ToString();
                actbAwayTeam.Value = dtoObj.AwayNationalTeam_Id.ToString();
            }
            else
            {
                actbHomeTeam.Value = dtoObj.HomeClub_Id.ToString();
                actbAwayTeam.Value = dtoObj.AwayClub_Id.ToString();
            }

            if (dtoObj.Referee != null)
            {
                actbReferee.Text = FormatName(dtoObj.Referee.FirstName, dtoObj.Referee.LastName, null);
                actbReferee.Value = dtoObj.Referee.Referee_Id.ToString();
            }
            ddlCompetitions.SelectedValue = dtoObj.Competition_Id.ToString();
            ddlSeasons.SelectedValue = dtoObj.Season_Id.ToString();
            tbDate.Text = FormatDate(dtoObj.Date);
            ddlStadiums.SelectedValue = dtoObj.Stadium.Stadium_ID.ToString();
            tbHomeTeamScore.Text = dtoObj.HomeScore.ToString();
            tbAwayTeamScore.Text = dtoObj.AwayScore.ToString();
            tbHomeTeamPenaltyScore.Text = dtoObj.HomePenaltyScore.ToString();
            tbAwayTeamPenaltyScore.Text = dtoObj.AwayPenaltyScore.ToString();
            tbSpecatators.Text = dtoObj.Spectators.ToString();
            tbSpecialNotes.Text = dtoObj.SpecialNote;
            SetAutocompleteTypes();
            SetCompetitionStages();
            if (dtoObj.CompetitionStage_Id.HasValue)
            {
                ddlStage.SelectedValue = dtoObj.CompetitionStage_Id.ToString();
            }

            List<Pair> uiLineup = new List<Pair>(23);

            if (isNewObject)
            {
                tbDate.Text = FormatDate(DateTime.Now);

                for (int i = 0; i < 25; i++)
                {
                    uiLineup.Add(new Pair(new MatchLineupDTO(), new MatchLineupDTO()));
                }
            }
            else
            {
                List<MatchLineupDTO> homePlayers = DataItem.Lineup.Where(l => l.IsHomeTeamPlayer && l.Player_Id != null).ToList();
                List<MatchLineupDTO> awayPlayers = DataItem.Lineup.Where(l => !l.IsHomeTeamPlayer && l.Player_Id != null).ToList();

                for (int i = 0; i < 30; i++)
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
            }

            rptLineup.DataSource = uiLineup;
            rptLineup.DataBind();

            MatchLineupDTO homeCoach = DataItem.Lineup.SingleOrDefault(l => l.IsHomeTeamPlayer && l.CoachId != null);
            MatchLineupDTO awayCoach = DataItem.Lineup.SingleOrDefault(l => !l.IsHomeTeamPlayer && l.CoachId != null);
            if (homeCoach != null)
            {
                hfHomeCoachLineupId.Value = homeCoach.MatchLineup_Id.ToString();
                actbHomeCoach.Value = homeCoach.CoachId.ToString();
                actbHomeCoach.Text = FormatName(homeCoach.Coach_FirstName, homeCoach.Coach_LastName, "");
            }
            if (awayCoach != null)
            {
                hfAwayCoachLineupId.Value = awayCoach.MatchLineup_Id.ToString();
                actbAwayCoach.Value = awayCoach.CoachId.ToString();
                actbAwayCoach.Text = FormatName(awayCoach.Coach_FirstName, awayCoach.Coach_LastName, "");
            }

            for (int i = 0; i < 16; i++)
            {
                if (DataItem.Events.Count < i)
                {
                    DataItem.Events.Add(new MatchEventDTO());
                }
            }

            rptEvents.DataSource = DataItem.Events;
            rptEvents.DataBind();

            if (DataItem.Flags.HasValue && DataItem.Flags.Value > 0)
            {
                foreach (KeyValuePair<int, string> flag in UIHelper.MatchFlagsMap)
                {
                    if ((DataItem.Flags & flag.Key) > 0)
                    {
                        ListItem cb = cblMatchFlags.Items.FindByValue(flag.Key.ToString());
                        if (cb != null)
                        {
                            cb.Selected = true;
                        }
                    }
                }
            }

        }

        protected override MatchDTO UIToDTO()
        {
            bool isNationalTeamMatch = cbMatchKind.Checked;
            int? homeTeamId = actbHomeTeam.Value.ParseInt(false);
            int? awayTeamId = actbAwayTeam.Value.ParseInt(false);
            short? homePenScore = tbHomeTeamPenaltyScore.Text.Length > 0 ? (short?)short.Parse(tbHomeTeamPenaltyScore.Text) : null;
            short? awayPenScore = tbAwayTeamPenaltyScore.Text.Length > 0 ? (short?)short.Parse(tbAwayTeamPenaltyScore.Text) : null;
            int? refereeId = actbReferee.Value.ParseInt(true);
            MatchDTO MatchToSave = new MatchDTO
            {
                HomeClub_Id = isNationalTeamMatch ? null : homeTeamId,
                HomeNationalTeam_Id = isNationalTeamMatch ? homeTeamId : null,
                AwayClub_Id = isNationalTeamMatch ? null : awayTeamId,
                AwayNationalTeam_Id = isNationalTeamMatch ? awayTeamId : null,
                HomeScore = short.Parse(tbHomeTeamScore.Text),
                AwayScore = short.Parse(tbAwayTeamScore.Text),
                HomePenaltyScore = homePenScore,
                AwayPenaltyScore = awayPenScore,
                Competition_Id = GetDropdownValue(ddlCompetitions).Value,
                Season_Id = GetDropdownValue(ddlSeasons).Value,
                Stadium = new StadiumDTO
                {
                    Stadium_ID = GetDropdownValue(ddlStadiums).Value
                },
                CompetitionStage_Id = ddlStage.SelectedValue.ParseInt(true),
                Match_Id = DataItem.Match_Id,
                Spectators = tbSpecatators.Text.ParseInt(true),
                Referee = refereeId == null ? null : new RefereeDTO
                {
                    Referee_Id = refereeId.Value
                },
                Lineup = new List<MatchLineupDTO>(),
                Events = new List<MatchEventDTO>(),
                SpecialNote = tbSpecialNotes.Text.Length > 0 ? tbSpecialNotes.Text : null
            };

            DateTime date = DateTime.Now;
            if (DateTime.TryParse(tbDate.Text, out date))
            {
                MatchToSave.Date = date;
            }

            int matchFlag = 0;
            foreach (ListItem li in cblMatchFlags.Items)
            {
                if (li.Selected)
                {
                    int flag = int.Parse(li.Value);
                    matchFlag |= flag;
                }
            }

            MatchToSave.Flags = matchFlag;

            int homeCountryId = GetCountryId(isNationalTeamMatch, homeTeamId);
            int awayCountryId = GetCountryId(isNationalTeamMatch, awayTeamId);

            foreach (RepeaterItem ri in rptLineup.Items)
            {
                AutocompleteTextBox actbHomePlayer = ri.FindControl("actbHomePlayer") as AutocompleteTextBox;
                AutocompleteTextBox actbAwayPlayer = ri.FindControl("actbAwayPlayer") as AutocompleteTextBox;
                TextBox tbHomePlayerShirtNumber = ri.FindControl("tbHomePlayerShirtNumber") as TextBox;
                TextBox tbAwayPlayerShirtNumber = ri.FindControl("tbAwayPlayerShirtNumber") as TextBox;
                HiddenField hfHomePlayerLineupId = ri.FindControl("hfHomePlayerLineupId") as HiddenField;
                HiddenField hfAwayPlayerLineupId = ri.FindControl("hfAwayPlayerLineupId") as HiddenField;

                if (actbHomePlayer.Text.Length > 0 && tbHomePlayerShirtNumber.Text.Length > 0)
                {
                    MatchLineupDTO hpml = GetLineup(actbHomePlayer, homeCountryId, int.Parse(tbHomePlayerShirtNumber.Text), int.Parse(hfHomePlayerLineupId.Value), ri.ItemIndex > 10, true);
                    MatchToSave.Lineup.Add(hpml);
                }
                if (actbAwayPlayer.Text.Length > 0 && tbAwayPlayerShirtNumber.Text.Length > 0)
                {
                    MatchLineupDTO apml = GetLineup(actbAwayPlayer, awayCountryId, int.Parse(tbAwayPlayerShirtNumber.Text), int.Parse(hfAwayPlayerLineupId.Value), ri.ItemIndex > 10, false);
                    MatchToSave.Lineup.Add(apml);
                }
            }

            if (!string.IsNullOrEmpty(actbHomeCoach.Value) && !string.IsNullOrEmpty(actbHomeCoach.Text))
            {
                MatchLineupDTO homeCoachLineupDTO = new MatchLineupDTO
                {
                    IsHomeTeamPlayer = true,
                    Match_Id = DataItem.Match_Id,
                    IsSubstitute = false,
                    CoachId = int.Parse(actbHomeCoach.Value),
                    ShirtNum = null,
                    MatchLineup_Id = string.IsNullOrEmpty(hfHomeCoachLineupId.Value) ? 0 : int.Parse(hfHomeCoachLineupId.Value)
                };
                MatchToSave.Lineup.Add(homeCoachLineupDTO);
            }

            if (!string.IsNullOrEmpty(actbAwayCoach.Value) && !string.IsNullOrEmpty(actbAwayCoach.Text))
            {
                MatchLineupDTO awayCoachLineupDTO = new MatchLineupDTO
                {
                    IsHomeTeamPlayer = false,
                    Match_Id = DataItem.Match_Id,
                    IsSubstitute = false,
                    CoachId = int.Parse(actbAwayCoach.Value),
                    ShirtNum = null,
                    MatchLineup_Id = string.IsNullOrEmpty(hfAwayCoachLineupId.Value) ? 0 : int.Parse(hfAwayCoachLineupId.Value)
                };
                MatchToSave.Lineup.Add(awayCoachLineupDTO);
            }

            foreach (RepeaterItem ri in rptEvents.Items)
            {
                TextBox tbMinute = ri.FindControl("tbMinute") as TextBox;
                HiddenField hfEventId = ri.FindControl("hfMatchEventId") as HiddenField;
                DropDownList ddlEventType = ri.FindControl("ddlEventTypeCd") as DropDownList;
                AutocompleteTextBox actbPlayer1 = ri.FindControl("actbEventPlayer1") as AutocompleteTextBox;
                AutocompleteTextBox actbPlayer2 = ri.FindControl("actbEventPlayer2") as AutocompleteTextBox;
                CheckBoxList cblEventFlags = ri.FindControl("cblEventFlags") as CheckBoxList;

                bool isValid = tbMinute.Text.Length > 0 && actbPlayer1.Value.Length > 0;
                if (ddlEventType.SelectedValue == Constants.DB.EventTypeCodes.Substitution)
                {
                    isValid &= actbPlayer2.Value.Length > 0;
                }

                if (isValid)
                {
                    MatchEventDTO newEvent = new MatchEventDTO
                    {
                        Event_Cd = ddlEventType.SelectedValue,
                        MatchEvent_Id = hfEventId.Value.Length > 0 ? int.Parse(hfEventId.Value) : 0,
                        Minute = int.Parse(tbMinute.Text),
                        Player1_Id = int.Parse(actbPlayer1.Value),
                        Player2_Id = actbPlayer2.Value.Length > 0 ? (int?)int.Parse(actbPlayer2.Value) : null,
                        Match_Id = DataItem.Match_Id
                    };

                    if (newEvent.Player1_Id > 0)
                    {
                        int eventFlags = 0;
                        foreach (ListItem item in cblEventFlags.Items)
                        {
                            if (item.Selected) eventFlags |= int.Parse(item.Value);
                        }

                        newEvent.EventFlags = eventFlags;
                        MatchToSave.Events.Add(newEvent);
                    }
                }

            }

            return MatchToSave;
        }

        private int GetCountryId(bool isNationalMatch, int? teamId)
        {
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                if (isNationalMatch)
                {
                    return db.NationalTeams.Single(nt => nt.NationalTeam_Id == teamId.Value).Country.Country_ID;
                }
                else
                {
                    return db.Clubs.Single(c => c.Club_ID == teamId.Value).City.Country_ID;
                }
            }
        }

        private MatchLineupDTO GetLineup(AutocompleteTextBox actbPlayer, int countryId, int shirtNum, int? lineupId, bool isSubstitute, bool isHomeTeamPlayer)
        {
            int playerId = 0;
            
            if (!actbPlayer.Value.IsEmpty() && !actbPlayer.Value.Equals("0"))
            {
                playerId = int.Parse(actbPlayer.Value);
            }
            else
            {
                if (actbPlayer.Text.Length > 0)
                {

                    string fName = null;
                    string lName = null;
                    int spaceIndex = actbPlayer.Text.IndexOf(' ');
                    if (spaceIndex > 1)
                    {
                        fName = actbPlayer.Text.Substring(0, spaceIndex);
                        lName = actbPlayer.Text.Substring(spaceIndex + 1);
                    }
                    else
                        lName = actbPlayer.Text;

                    

                    PlayerDTO newPlayer = new PlayerDTO()
                    {
                        First_Name = fName,
                        Last_Name = lName,
                        RequiresReview = true,
                        Country_Id = countryId
                    };

                    playerId = new PlayerDTOHelper().SaveToDB(newPlayer);
                }
            }

            MatchLineupDTO hpml = new MatchLineupDTO
            {
                IsHomeTeamPlayer = isHomeTeamPlayer,
                Match_Id = DataItem.Match_Id,
                IsSubstitute = isSubstitute,
                Player_Id = playerId,
                ShirtNum = shirtNum
            };

            
            if (lineupId.HasValue) hpml.MatchLineup_Id = lineupId.Value;

            return hpml;
        }


        protected void cbMatchKind_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbMatchKind = sender as CheckBox;
            bool isNationalTeamMatch = cbMatchKind.Checked;
            string competitionFilter = isNationalTeamMatch ? Constants.DB.CompetitionLevelCd_NationalTeam : Constants.DB.CompetitionLevelCd_Club;

            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                BindDropdown(db, Constants.ObjectType.Competition, ddlCompetitions, true, competitionFilter);
                BindDropdown(db, Constants.ObjectType.Season, ddlSeasons, true, competitionFilter);
            }
        }

        protected void SetAutocompleteTypes()
        {
            if (cbMatchKind.Checked)
            {
                actbAwayTeam.AutocompleteType = AutocompleteType.NationalTeam;
                actbHomeTeam.AutocompleteType = AutocompleteType.NationalTeam;
            }
            else
            {
                actbAwayTeam.AutocompleteType = AutocompleteType.Club;
                actbHomeTeam.AutocompleteType = AutocompleteType.Club;
            }
            
        }

        protected void SetCompetitionStages()
        {
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                BindDropdown(db, Constants.ObjectType.CompetitionStage, ddlStage, true, ddlCompetitions.SelectedValue);
            }
        }

        protected void ddlCompetitions_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCompetitionStages();
        }

        protected void ddlEventTypeCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CheckBoxList cblCompetitionStages
            DropDownList ddlEventTypeCd = sender as DropDownList;
            CheckBoxList cblEventTypeFlags = ddlEventTypeCd.Parent.FindControl("cblEventFlags") as CheckBoxList;

            if (UIHelper.EventCodeEventFlagsMap.ContainsKey(ddlEventTypeCd.SelectedValue))
            {
                Dictionary<int, string> eventFlags = UIHelper.EventCodeEventFlagsMap[ddlEventTypeCd.SelectedValue];
                cblEventTypeFlags.DataSource = eventFlags;
                cblEventTypeFlags.DataTextField = "Value";
                cblEventTypeFlags.DataValueField = "Key";
                cblEventTypeFlags.DataBind();
            }

            
        }

        protected void rptLineup_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            AutocompleteTextBox actbHomePlayer = e.Item.FindControl("actbHomePlayer") as AutocompleteTextBox;
            AutocompleteTextBox actbAwayPlayer = e.Item.FindControl("actbAwayPlayer") as AutocompleteTextBox;
            actbAwayPlayer.BehaviorId = "awayPlayerAutocomplete" + e.Item.ItemIndex.ToString();
            actbHomePlayer.BehaviorId = "homePlayerAutocomplete" + e.Item.ItemIndex.ToString();
            //TextBox tbHomePlayerShirtNum = e.Item.FindControl("tbHomePlayerShirtNumber") as TextBox;
            //TextBox tbAwayPlayerShirtNum = e.Item.FindControl("tbAwayPlayerShirtNumber") as TextBox;
            //tbHomePlayerShirtNum.ID = "tbHomePlayerShirtNum" + e.Item.ItemIndex.ToString();
            //tbAwayPlayerShirtNum.ID = "tbAwayPlayerShirtNum" + e.Item.ItemIndex.ToString();
            if (e.Item.ItemIndex == 11)
            {
                HtmlTableRow tr = e.Item.FindControl("trLinup") as HtmlTableRow;
                tr.Attributes.Add("class", "trSub");
            }
        }

        protected void rptEvents_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            AutocompleteTextBox actbPlayer1 = e.Item.FindControl("actbEventPlayer1") as AutocompleteTextBox;
            AutocompleteTextBox actbPlayer2 = e.Item.FindControl("actbEventPlayer2") as AutocompleteTextBox;
            actbPlayer1.BehaviorId = "actbEventPlayer1" + e.Item.ItemIndex.ToString();
            actbPlayer2.BehaviorId = "actbEventPlayer2" + e.Item.ItemIndex.ToString();

            DropDownList ddlEventTypes = e.Item.FindControl("ddlEventTypeCd") as DropDownList;
            ddlEventTypes.DataBound += new EventHandler(ddlEventTypes_DataBound);
            ddlEventTypes.DataTextField = "Value";
            ddlEventTypes.DataValueField = "Key";
            ddlEventTypes.DataSource = UIHelper.EventCodeMap;
            ddlEventTypes.DataBind();
            //CheckBoxList cblCardFlags = e.Item.FindControl("cblCardFlags") as CheckBoxList;


        }

        void ddlEventTypes_DataBound(object sender, EventArgs e)
        {
            DropDownList ddlEventTypes = sender as DropDownList;
            ddlEventTypes.Items.Insert(0, new ListItem(Constants.UI.DropdownDefaultText, Constants.UI.DropdownDefaultValue));
        }

        protected void rptLineup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Pair uiPair = e.Item.DataItem as Pair;
            MatchLineupDTO homePlayer = uiPair.First as MatchLineupDTO;
            MatchLineupDTO awayPlayer = uiPair.Second as MatchLineupDTO;

            AutocompleteTextBox actbHomePlayer = e.Item.FindControl("actbHomePlayer") as AutocompleteTextBox;
            AutocompleteTextBox actbAwayPlayer = e.Item.FindControl("actbAwayPlayer") as AutocompleteTextBox;
            TextBox tbHomePlayerShirtNumber = e.Item.FindControl("tbHomePlayerShirtNumber") as TextBox;
            TextBox tbAwayPlayerShirtNumber = e.Item.FindControl("tbAwayPlayerShirtNumber") as TextBox;
            HiddenField hfHomePlayerLineupId = e.Item.FindControl("hfHomePlayerLineupId") as HiddenField;
            HiddenField hfAwayPlayerLineupId = e.Item.FindControl("hfAwayPlayerLineupId") as HiddenField;

            actbHomePlayer.Value = homePlayer.Player_Id.ToString();
            if (homePlayer.Player_Id > 0)
            {
                actbHomePlayer.Text = FormatName(homePlayer.Player_FirstName, homePlayer.Player_LastName, homePlayer.Player_DisplayName);
            }
            tbHomePlayerShirtNumber.Text = homePlayer.ShirtNum.ToString();
            hfHomePlayerLineupId.Value = homePlayer.MatchLineup_Id.ToString();

            actbAwayPlayer.Value = awayPlayer.Player_Id.ToString();
            if (awayPlayer.Player_Id > 0)
            {
                actbAwayPlayer.Text = FormatName(awayPlayer.Player_FirstName, awayPlayer.Player_LastName, awayPlayer.Player_DisplayName);
            }
            tbAwayPlayerShirtNumber.Text = awayPlayer.ShirtNum.ToString();
            hfAwayPlayerLineupId.Value = awayPlayer.MatchLineup_Id.ToString();
            
        }

        protected void rptEvents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            MatchEventDTO ev = e.Item.DataItem as MatchEventDTO;

            DropDownList ddlEventTypeCd = e.Item.FindControl("ddlEventTypeCd") as DropDownList;
            CheckBoxList cblEvents = e.Item.FindControl("cblEventFlags") as CheckBoxList;

            if (ev != null && ddlEventTypeCd != null && cblEvents != null && ev.Event_Cd!=null)
            {
                if (ev.Event_Cd != Constants.UI.DropdownDefaultValue)
                {
                    ddlEventTypeCd.SelectedValue = ev.Event_Cd;
                    cblEvents.DataSource = UIHelper.EventCodeEventFlagsMap[ev.Event_Cd];
                    cblEvents.DataTextField = "Value";
                    cblEvents.DataValueField = "Key";
                    cblEvents.DataBind();

                    if (ev.EventFlags.HasValue)
                    {
                        foreach (ListItem li in cblEvents.Items)
                        {
                            int flag = int.Parse(li.Value);
                            li.Selected = (ev.EventFlags.Value & flag) > 0;
                        }
                    }
                }
            }
        }

        protected void cblEventFlags_DataBound(object sender, EventArgs e)
        {

        }

        protected void btnAddEvent_Click(object sender, EventArgs e)
        {
            //foreach (RepeaterItem item in rptEvents.Items)
            //{
            //    TextBox tbMinute = item.FindControl("tbMinute") as TextBox;
            //    HiddenField hfMatchEventId = item.FindControl("hfMatchEventId") as HiddenField;
            //    DropDownList ddlEventTypeCd = item.FindControl("ddlEventTypeCd") as DropDownList;
            //    AutocompleteTextBox actbPlayer1 = item.FindControl("actbEventPlayer1") as AutocompleteTextBox;
            //    AutocompleteTextBox actbPlayer2 = item.FindControl("actbEventPlayer2") as AutocompleteTextBox;

            //    if (hfMatchEventId.Value == "0")
            //    {
            //        MatchEventDTO newEvent = new MatchEventDTO
            //        {
            //            Event_Cd = ddlEventTypeCd.SelectedValue,
            //            MatchEvent_Id = hfMatchEventId.Value.Length > 0 ? int.Parse(hfMatchEventId.Value) : 0,
            //            Minute = int.Parse(tbMinute.Text),
            //            Player1_Id = int.Parse(actbPlayer1.Value),
            //            Player2_Id = actbPlayer2.Value.Length > 0 ? (int?)int.Parse(actbPlayer2.Value) : null,
            //            Match_Id = DataItem.Match_Id,

            //        };

            //        DataItem.Events.Add(newEvent);
            //    }
            //}
            for (int i = 0; i < 8; i++)
            {
                DataItem.Events.Add(new MatchEventDTO());
            }
            rptEvents.DataSource = DataItem.Events;
            rptEvents.DataBind();
        }
    } 
}