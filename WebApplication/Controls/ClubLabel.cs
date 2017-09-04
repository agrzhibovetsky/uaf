using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using UaFootball.AppCode;

namespace UaFootball.WebApplication.Controls
{
    public class ClubLabel : Label
    {
        public string CountryCode { get; set; }

        private const string _UATeamClass = "uaTeam";

        protected override void OnPreRender (EventArgs e)
        {
            if (CountryCode != null)
            {
                if (CountryCode.Equals(Constants.DB.UACountryCode))
                {
                    CssClass = _UATeamClass;
                }
            }
            base.OnLoad(e);
        }
    }
}