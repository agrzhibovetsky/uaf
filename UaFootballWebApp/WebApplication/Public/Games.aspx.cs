using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;
using UaFDatabase;

namespace UaFootball.WebApplication
{
    public partial class Games : UaFootballPageBase
    {

        private bool _isNationalTeam;

        public bool IsNationalTeam
        {
            get {return _isNationalTeam;}
            set { _isNationalTeam = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            IsNationalTeam = (Request[Constants.QueryParam.NationalTeam] != null && Request[Constants.QueryParam.NationalTeam].Equals("1"));

            string  competitionLevelCode = IsNationalTeam ? Constants.DB.CompetitionLevelCd_NationalTeam : Constants.DB.CompetitionLevelCd_Club;

            if (!IsPostBack)
            {
                using (UaFootball_DBDataContext db = DBManager.GetDB())
                {
                    ddlCompetitions.DataSource = GetGenericReferenceData(db, Constants.ObjectType.Competition).Where(grd => grd.GenericStringValue.Equals(competitionLevelCode));
                    ddlCompetitions.DataTextField = "Name";
                    ddlCompetitions.DataValueField = "Value";
                    ddlCompetitions.DataBind();

                    IEnumerable<GenericReferenceObject> seasons = GetGenericReferenceData(db, Constants.ObjectType.Season).Where(grd => grd.GenericStringValue.Equals(competitionLevelCode));
                    ddlSeasons.DataSource = seasons;
                    ddlSeasons.DataTextField = "Name";
                    ddlSeasons.DataValueField = "Value";
                    ddlSeasons.DataBind();
                    ddlSeasons.SelectedValue = seasons.FirstOrDefault().Value.ToString();
                    
                    ddlCompetitions.Items.Insert(0, new ListItem(Constants.UI.DropdownDefaultText, Constants.UI.DropdownDefaultValue));
                    ddlSeasons.Items.Insert(0, new ListItem(Constants.UI.DropdownDefaultText, Constants.UI.DropdownDefaultValue));

                    SearchParameters.Match searchParam = new SearchParameters.Match { CompetitionCode = competitionLevelCode, Season_Id = seasons.FirstOrDefault().Value };
                    BindData(searchParam);
                }
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            string competitionLevelCode = IsNationalTeam ? Constants.DB.CompetitionLevelCd_NationalTeam : Constants.DB.CompetitionLevelCd_Club;

            SearchParameters.Match searchParam = new SearchParameters.Match { CompetitionCode = competitionLevelCode };

            if (ddlCompetitions.SelectedItem.Value != Constants.UI.DropdownDefaultValue)
            {
                searchParam.Competition_Id = int.Parse(ddlCompetitions.SelectedItem.Value);
            }

            if (ddlSeasons.SelectedItem.Value != Constants.UI.DropdownDefaultValue)
            {
                searchParam.Season_Id = int.Parse(ddlSeasons.SelectedItem.Value);
            }

            if (!string.IsNullOrEmpty(ddlClubs.SelectedValue) && ddlClubs.SelectedValue != Constants.UI.DropdownDefaultValue)
            {
                searchParam.Club_Id = int.Parse(ddlClubs.SelectedValue);
            }

            BindData(searchParam);
        }

        private void BindData(SearchParameters.Match searchParam)
        {
            
            List<MatchDTO> matches = new MatchDTOHelper().GetListFromDB(searchParam);
            if (searchParam.Club_Id == 0)
            {
                if (!IsNationalTeam)
                {
                    IEnumerable<ClubDTO> awayClubs = matches.Select(m => new ClubDTO { Club_ID = m.AwayClub_Id.Value, Club_Name = m.AwayTeamName, IsUA = m.AwayTeamCountryCode == Constants.CountryCodeUA });
                    IEnumerable<ClubDTO> homeClubs = matches.Select(m => new ClubDTO { Club_ID = m.HomeClub_Id.Value, Club_Name = m.HomeTeamName, IsUA = m.HomeTeamCountryCode == Constants.CountryCodeUA });
                    IEnumerable<ClubDTO> allClubs = awayClubs.Union(homeClubs).Distinct(new ClubDTOComparer());
                    IEnumerable<ClubDTO> uaClubs = allClubs.Where(c => c.IsUA);
                    IEnumerable<ClubDTO> foreignClubs = allClubs.Where(c => !c.IsUA);

                    ddlClubs.Items.Clear();
                    ddlClubs.Items.Insert(0, new ListItem("Клуб", Constants.UI.DropdownDefaultValue));
                    ddlClubs.Items.AddRange(uaClubs.Select(c => new ListItem(c.Club_Name, c.Club_ID.ToString())).ToArray());
                    ddlClubs.Items.Add(new ListItem("--------", Constants.UI.DropdownDefaultValue));
                    ddlClubs.Items.AddRange(foreignClubs.Select(c => new ListItem(c.Club_Name, c.Club_ID.ToString())).ToArray());
                }
            }

            ml.DataSource = matches;
            ml.DataBind();

        }
    }
}