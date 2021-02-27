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
    public partial class NationalTeam : UaFootballPageBase
    {
        protected NationalTeamDTO DataItem
        {
            get
            {
                if (ViewState["DataItem"] != null)
                    return ViewState["DataItem"] as NationalTeamDTO;
                else return new NationalTeamDTO();
            }
            set
            {
                ViewState["DataItem"] = value;
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
                        NationalTeamDTOHelper nationalTeamDTOHelper = new NationalTeamDTOHelper();
                        try
                        {
                            DataItem = nationalTeamDTOHelper.GetFromDB(objectId);
                            if (DataItem.Logo != null)
                            {
                                iNationalTeamLogo.ImageUrl = PathHelper.GetMultimediaWebPath(this, DataItem.Logo);
                            }
                        }
                        catch (Exception ex)
                        {
                            //logger
                        }

                        SearchParameters.Match searchParam = new SearchParameters.Match();
                        searchParam.NationalTeam_Id = objectId;
                        ml.DataBind(searchParam);

                        using (UaFootball_DBDataContext db = DBManager.GetDB())
                        {
                            IEnumerable<vGamesByPlayerByTeam> playersForClub = db.vGamesByPlayerByTeams.Where(d => d.IsNational == 1 && d.PlayedFor == objectId).OrderByDescending(d=>d.TotalMatches).ThenByDescending(d=>d.TotalMinutes);
                            rptPlayers.DataSource = playersForClub;
                            rptPlayers.DataBind();
                        }
                    }
                }
            }
        }
    }
}