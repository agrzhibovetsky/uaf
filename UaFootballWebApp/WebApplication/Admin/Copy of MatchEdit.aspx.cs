using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MatchEdit : ObjectEditPageBase<MatchDTO>
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
        PrepareAutocompleteCustomValidator(cvHomeTeam, hfHomeTeamId);
        PrepareAutocompleteCustomValidator(cvAwayTeam, hfAwayTeamId);
        base.OnLoad(e);
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

    }

    protected override void DTOToUI(MatchDTO dtoObj)
    {
        bool isNationalTeamMatch = dtoObj.HomeNationalTeam_Id.HasValue;
        cbMatchKind.Checked = isNationalTeamMatch;
        tbHomeTeam.Text = dtoObj.HomeTeamName;
        tbAwayTeam.Text = dtoObj.AwayTeamName;
        hfHomeTeamId.Value = dtoObj.HomeClub_Id.ToString();
        hfAwayTeamId.Value = dtoObj.AwayClub_Id.ToString();
        ddlCompetitions.SelectedValue = dtoObj.Competition_Id.ToString();
        ddlSeasons.SelectedValue = dtoObj.Season_Id.ToString();
        tbDate.Text = FormatDate(dtoObj.Date);
        ddlStadiums.SelectedValue = dtoObj.Stadium_Id.ToString();
        tbHomeTeamScore.Text = dtoObj.HomeScore.ToString();
        tbAwayTeamScore.Text = dtoObj.AwayScore.ToString();
        tbHomeTeamPenaltyScore.Text = dtoObj.HomePenaltyScore.ToString();
        tbAwayTeamPenaltyScore.Text = dtoObj.AwayPenaltyScore.ToString();
    }

    protected override MatchDTO UIToDTO()
    {
        bool isNationalTeamMatch = cbMatchKind.Checked;
        int? homeTeamId = (int?) int.Parse(hfHomeTeamId.Value);
        int? awayTeamId = (int?) int.Parse(hfAwayTeamId.Value);
        short? homePenScore = tbHomeTeamPenaltyScore.Text.Length > 0 ? (short?) short.Parse(tbHomeTeamPenaltyScore.Text) : null;
        short? awayPenScore = tbAwayTeamPenaltyScore.Text.Length > 0 ? (short?) short.Parse(tbAwayTeamPenaltyScore.Text) : null;

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
            Stadium_Id = GetDropdownValue(ddlStadiums).Value
        };

        DateTime date = DateTime.Now;
        if (DateTime.TryParse(tbDate.Text, out date))
        {
            MatchToSave.Date = date;
        }

        return MatchToSave;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

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
}