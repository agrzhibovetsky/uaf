using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.DB;
using AjaxControlToolkit;

namespace UaFootball.WebApplication.Public
{
    public partial class Countries : System.Web.UI.Page
    {
        private List<UaFootball.DB.Country> _countryCache;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                _countryCache = db.Countries.ToList();
                List<FIFAAssociation> ass = db.FIFAAssociations.ToList();

                var countriesGroupped = _countryCache.GroupBy(c => c.FIFAAssociation_ID);
                
                var assOrdered2 = from association in ass
                                  join c in countriesGroupped on association.FIFAAssociation_ID equals c.Key
                                  select new { a = association, b = c.Count() };

                accAssociations.DataSource = assOrdered2.OrderByDescending(ao => ao.b).Select(ao => ao.a);
                accAssociations.DataBind();
            }
        }

        protected void accAssociations_ItemDataBound(object sender, AccordionItemEventArgs e)
        {
            FIFAAssociation curAss = e.AccordionItem.DataItem as FIFAAssociation;

            if (e.AccordionItem.ItemType == AccordionItemType.Header)
            {
                Label lblCount = e.AccordionItem.FindControl("lblCount") as Label;
                int countriesCount = _countryCache.Where(c => c.FIFAAssociation_ID == curAss.FIFAAssociation_ID).Count();
                switch (countriesCount)
                {
                    case 0:
                        lblCount.Text = "0 стран"; break;
                    case 1:
                        lblCount.Text = "1 страна"; break;
                    case 2:
                    case 3:
                    case 4:
                        lblCount.Text = countriesCount.ToString() + " страны"; break;
                    default: 
                        lblCount.Text = countriesCount.ToString() + " стран"; break;
                }
            }

            if (e.AccordionItem.ItemType == AccordionItemType.Content)
            {
                
                Repeater rptCountries = e.AccordionItem.FindControl("rptCountries") as Repeater;
                rptCountries.DataSource = _countryCache.Where(c => c.FIFAAssociation_ID == curAss.FIFAAssociation_ID).OrderBy(c => c.Country_Name);
                rptCountries.DataBind();
            }
        }

        protected void rptCountries_DataItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HyperLink hlCounry = e.Item.FindControl("hlCountry") as HyperLink;
            UaFootball.DB.Country c = e.Item.DataItem as UaFootball.DB.Country;
            hlCounry.Text = c.Country_Name;
            hlCounry.NavigateUrl = "~/WebApplication/Public/Country.aspx?countryId=" + c.Country_ID;
        }
    }
}