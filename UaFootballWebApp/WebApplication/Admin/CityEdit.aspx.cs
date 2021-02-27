using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using UaFootball.AppCode;
using UaFDatabase;

namespace UaFootball.WebApplication
{
    public partial class CityEdit : ObjectEditPageBase<CityDTO>
    {

        protected override string ObjectListPage
        {
            get { return Constants.Pages.List_City; }
        }

        protected override void PrepareUI()
        {
            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                BindDropdown(db, Constants.ObjectType.Country, ddlCountries, false, string.Empty);
            }
        }

        protected override void DTOToUI(CityDTO dtoObj)
        {
            if (dtoObj.City_ID > 0)
            {
                tbName.Text = dtoObj.City_Name;
                ddlCountries.SelectedValue = dtoObj.Country_ID.ToString();
            }
        }

        protected override CityDTO UIToDTO()
        {
            CityDTO CityToSave = new CityDTO();

            CityToSave.City_ID = DataItem.City_ID;
            CityToSave.City_Name = tbName.Text;
            CityToSave.Country_ID = int.Parse(ddlCountries.SelectedValue);

            return CityToSave;
        }
    } 
}