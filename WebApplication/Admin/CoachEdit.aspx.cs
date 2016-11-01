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
    public partial class CoachEdit : ObjectEditPageBase<CoachDTO>
    {
        protected override string ObjectListPage
        {
            get { return Constants.Pages.List_Coach; }
        }

        protected override void PrepareUI()
        {
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                BindDropdown(db, Constants.ObjectType.Country, ddlCountries, false, string.Empty);
            }
        }

        protected override void DTOToUI(CoachDTO dtoObj)
        {
            if (dtoObj.CoachId > 0)
            {
                tbFirstName.Text = dtoObj.FirstName;
                tbLastName.Text = dtoObj.LastName;
                tbDOB.Text = FormatDate(dtoObj.DOB);
                ddlCountries.SelectedValue = dtoObj.Country_Id.ToString();
                tbFirstNameEN.Text = dtoObj.FirstName_EN;
                tbLastNameEN.Text = dtoObj.LastName_EN;
            }
        }

        protected override CoachDTO UIToDTO()
        {
            CoachDTO CoachToSave = new CoachDTO()
            {
                Country_Id = int.Parse(ddlCountries.SelectedValue),
                FirstName = tbFirstName.Text.Trim(),
                LastName = tbLastName.Text.Trim(),
                CoachId = DataItem.CoachId,
                FirstName_EN = tbFirstNameEN.Text.Trim(),
                LastName_EN = tbLastNameEN.Text.Trim()
            };

            DateTime DOB = DateTime.Now;
            if (DateTime.TryParse(tbDOB.Text, out DOB))
            {
                CoachToSave.DOB = DOB;
            }
            return CoachToSave;
        }
    } 
}