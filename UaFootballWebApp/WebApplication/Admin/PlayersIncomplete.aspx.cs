using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFDatabase;
using UaFootball.AppCode;

namespace UaFootball.WebApplication.Admin
{
    public partial class PlayersIncomplete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                var playersNoCity = db.Players.Where(p => p.Country.Country_Code == Constants.CountryCodeUA && (p.UACity_Name == string.Empty || p.UACity_Name == null));
                rptNoCity.DataSource = playersNoCity.OrderByDescending(p=>p.LastUpdate_DT);
                rptNoCity.DataBind();

                var playersNoPhoto = db.Players.Where(p => p.Country.Country_Code == Constants.CountryCodeUA && (p.MultimediaTags.FirstOrDefault(mt => mt.Multimedia.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.PlayerLogo) == null));
                rptNoPhoto.DataSource = playersNoPhoto.OrderByDescending(p => p.LastUpdate_DT);
                rptNoPhoto.DataBind();

                var playersNoHeight = db.Players.Where(p => p.Country.Country_Code == Constants.CountryCodeUA && !p.Height.HasValue);
                rptNoHeight.DataSource = playersNoHeight.OrderByDescending(p => p.LastUpdate_DT);
                rptNoHeight.DataBind();

                //db.Players.First().

            }
        }
    }
}