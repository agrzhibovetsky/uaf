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
    public partial class Club : UaFootballPageBase
    {
        protected ClubDTO DataItem
        {
            get
            {
                if (ViewState["DataItem"] != null)
                    return ViewState["DataItem"] as ClubDTO;
                else return new ClubDTO();
            }
            set
            {
                ViewState["DataItem"] = value;
            }
        }

        protected string ClubAge
        {
            get
            {
                if (DataItem.Year_Found.HasValue && DataItem.Year_Found > 1800)
                {
                    int yearsAge = DateTime.Now.Year - DataItem.Year_Found.Value;
                    string yearsStg = "лет";
                    int yearsEnd = yearsAge % 10;
                    switch (yearsEnd)
                    {
                        case 1:
                            yearsStg = "год"; break;
                        case 2:
                        case 3:
                        case 4:
                            yearsStg = "года"; break;
                    }
                    return string.Format("{0} {1}", yearsAge, yearsStg);
                }
                else
                {
                    return "Нет данных";
                }

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string objIdParam = Request["id"];
                if (!string.IsNullOrEmpty(objIdParam))
                {
                    int objectId = 0;
                    if (int.TryParse(objIdParam, out objectId))
                    {
                        ClubDTOHelper clubDTOHelper = new ClubDTOHelper();
                        try
                        {
                            DataItem = clubDTOHelper.GetFromDB(objectId);
                            if (DataItem.Logo != null)
                            {
                                iClubLogo.ImageUrl = PathHelper.GetMultimediaWebPath(this, DataItem.Logo);
                            }
                        }
                        catch (Exception ex)
                        {
                            //logger
                        }

                        SearchParameters.Match searchParam = new SearchParameters.Match();
                        searchParam.Club_Id = objectId;
                        ml.DataBind(searchParam);

                        using (UaFootball_DBDataContext db = DBManager.GetDB())
                        {
                            IEnumerable<vGamesByPlayerByTeam> playersForClub = db.vGamesByPlayerByTeams.Where(d => d.IsNational == 0 && d.PlayedFor == objectId).OrderByDescending(d=>d.TotalMatches).ThenByDescending(d=>d.TotalMinutes);
                            rptPlayers.DataSource = playersForClub;
                            rptPlayers.DataBind();
                        }
                    }
                }
            }
        }
    }
}