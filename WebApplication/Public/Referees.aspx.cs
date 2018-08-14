using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.DB;

namespace UaFootball.WebApplication
{
    public partial class Referees : UaFootballPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                var allReferees = db.vw_RefereeLists.GroupBy(cl => cl.Country_Name).ToList();
                rptCountries.DataSource = allReferees;
                rptCountries.DataBind();
            }
        }

        protected void rptCountries_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            IGrouping<string, vw_RefereeList> data = e.Item.DataItem as IGrouping<string, vw_RefereeList>;
            if (data != null)
            {
                Literal ltCountryName = e.Item.FindControl("ltCountryName") as Literal;
                ltCountryName.Text = data.Key;
                Repeater rptCountryReferees = e.Item.FindControl("rptCountryReferees") as Repeater;
                rptCountryReferees.DataSource = data.OrderBy(d=>d.LastName);
                rptCountryReferees.DataBind();
            }
        }
    }
}