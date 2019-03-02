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
    public partial class StadiumEdit : ObjectEditPageBase<StadiumDTO>
    {
        protected override string ObjectListPage
        {
            get { return Constants.Pages.List_Stadium; }
        }

        protected override void PrepareUI()
        {
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                BindDropdown(db, Constants.ObjectType.City, ddlCities, false, string.Empty);
            }
        }

        protected override void DTOToUI(StadiumDTO dtoObj)
        {
            if (dtoObj.Stadium_ID > 0)
            {
                tbName.Text = dtoObj.Stadium_Name;
                tbCapacity.Text = dtoObj.Capacity.ToString();
                tbYearBuilt.Text = dtoObj.YearBuilt.ToString();
                tbComments.Text = dtoObj.Comments;
                ddlCities.SelectedValue = dtoObj.City_ID.ToString();
            }
        }

        protected override StadiumDTO UIToDTO()
        {
            StadiumDTO stadiumToSave = new StadiumDTO();

            stadiumToSave.Stadium_ID = DataItem.Stadium_ID;
            stadiumToSave.Stadium_Name = tbName.Text;
            stadiumToSave.Capacity = int.Parse(tbCapacity.Text);
            stadiumToSave.YearBuilt = int.Parse(tbYearBuilt.Text);
            stadiumToSave.City_ID = int.Parse(ddlCities.SelectedValue);
            stadiumToSave.Comments = tbComments.Text;
            return stadiumToSave;
        }
    } 
}