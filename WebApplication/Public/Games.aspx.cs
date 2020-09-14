using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;
using UaFootball.DB;

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
                using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
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
                    ml.DataBind(searchParam);
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

            ml.DataBind(searchParam);
        }
    }
}