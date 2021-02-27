using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFDatabase;
using UaFootball.AppCode;

namespace UaFootball.WebApplication.Public
{
    public partial class Coaches : UaFootballPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                //var allCoaches = db.vw_CoachesLists.GroupBy(cl => cl.Country_Name).Select(cl=>new {CountryName = cl.Key, Referees = cl}).ToList();
                var allCoaches = db.vw_CoachesLists.GroupBy(cl => cl.Country_Name).ToList();
                var b = allCoaches.First();
                rptCountries.DataSource = allCoaches;
                rptCountries.DataBind();
            }
        }

        protected void rptCountries_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            IGrouping<string, vw_CoachesList> data = e.Item.DataItem as IGrouping<string, vw_CoachesList>;
            if (data != null)
            {
                Literal ltCountryName = e.Item.FindControl("ltCountryName") as Literal;
                ltCountryName.Text = data.Key;
                Repeater rptCountryCoaches = e.Item.FindControl("rptCountryCoaches") as Repeater;
                rptCountryCoaches.DataSource = data;
                rptCountryCoaches.DataBind();
            }
        }
    }
}