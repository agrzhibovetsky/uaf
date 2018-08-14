using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using UaFootball.AppCode;
using UaFootball.DB;

namespace UaFootball.WebApplication
{
    public partial class Player : UaFootballPageBase
    {
        #region Props
        protected PlayerDTO DataItem
        {
            get
            {
                if (ViewState["DataItem"] != null)
                    return ViewState["DataItem"] as PlayerDTO;
                else return new PlayerDTO();
            }
            set
            {
                ViewState["DataItem"] = value;
            }
        }

        protected int EuropcupsMatchesCount { get; set; }
        protected int NationalTeamMatchesCount { get; set; }
        protected int PhotoCount { get; set; }
        protected int VideoCount { get; set; }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptInclude(Constants.Paths.JQueryKey, Page.ResolveClientUrl(Constants.Paths.JQueryPath));
            Page.ClientScript.RegisterClientScriptInclude(Constants.Paths.JQueryUIKey, Page.ResolveClientUrl(Constants.Paths.JQueryUIPath));
            Page.ClientScript.RegisterClientScriptInclude(Constants.Paths.ColorboxKey, Page.ResolveClientUrl(Constants.Paths.ColorboxPath));

            if (!IsPostBack)
            {
                if (Request[Constants.QueryParam.PlayerId].Length > 0)
                {
                    int playerId = int.Parse(Request[Constants.QueryParam.PlayerId]);
                    DataItem = new PlayerDTOHelper().GetPlayer(playerId);

                    string thumbPath = "\\thumb\\";

                    IEnumerable<MultimediaDTO> playerLogos = DataItem.Multimedia.Where(m => m.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.PlayerLogo);
                    if (playerLogos.Count() > 0)
                    {
                        MultimediaDTO logo = playerLogos.OrderByDescending(pl => pl.Multimedia_ID).First();
                        iPlayerLogo.ImageUrl = PathHelper.GetWebPath(this, Constants.Paths.MutlimediaWebRoot, logo.FilePath + thumbPath, logo.FileName);
                    }
                    else
                    {
                        iPlayerLogo.ImageUrl = ResolveClientUrl("~/WebApplication/images/nofoto.jpg");
                    }

                    mlpLast20.PlayerId = playerId;
                    mlpLast20.DataSource = DataItem.Matches.OrderByDescending(m => m.Date).Take(20);
                    mlpLast20.DataBind();

                    IEnumerable<MatchDTO> nationalTeamMatches = DataItem.Matches.Where(m => m.IsNationalTeamMatch);
                    mlpSBU.PlayerId = playerId;
                    mlpSBU.DataSource = nationalTeamMatches.OrderByDescending(m => m.Date);
                    mlpSBU.DataBind();
                    
                    IEnumerable<MatchDTO> europcupMatches = DataItem.Matches.Where(m => !m.IsNationalTeamMatch);
                    mlpClub.PlayerId = playerId;
                    mlpClub.DataSource = europcupMatches.OrderByDescending(m => m.Date);
                    mlpClub.DataBind();

                    NationalTeamMatchesCount = nationalTeamMatches.Count();
                    EuropcupsMatchesCount = europcupMatches.Count();
                    PhotoCount = DataItem.Multimedia.Count(mm => mm.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.MatchPhoto);
                    VideoCount = DataItem.Multimedia.Count(mm => mm.MultimediaType_CD == Constants.DB.MutlimediaTypes.Video);
                }
            }
        }

    }
}