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
    public partial class CountryEdit : ObjectEditPageBase<CountryDTO>
    {
        protected override string ObjectListPage
        {
            get { return Constants.Pages.List_Country; }
        }

        protected override void PrepareUI()
        {
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                BindDropdown(db, Constants.ObjectType.FIFAAssociation, ddlConfedereations, false, string.Empty);
            }
        }

        protected override void DTOToUI(CountryDTO dtoObj)
        {
            if (dtoObj.Country_ID > 0)
            {
                tbName.Text = dtoObj.Country_Name;
                tbCode.Text = dtoObj.Country_Code;
                ddlConfedereations.SelectedValue = dtoObj.FIFAAssociation_ID.ToString();
            }
        }

        protected override CountryDTO UIToDTO()
        {
            CountryDTO countryToSave = new CountryDTO();

            countryToSave.Country_ID = DataItem.Country_ID;
            countryToSave.Country_Name = tbName.Text;
            countryToSave.Country_Code = tbCode.Text;
            countryToSave.FIFAAssociation_ID = int.Parse(ddlConfedereations.SelectedValue);

            return countryToSave;
        }
    } 
}