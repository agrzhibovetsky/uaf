using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;
using UaFDatabase;

namespace UaFootball.WebApplication
{
    public partial class RefereeEdit : ObjectEditPageBase<RefereeDTO>
    {
        protected override string ObjectListPage
        {
            get { return Constants.Pages.List_Referee; }
        }

        protected override void PrepareUI()
        {
            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                BindDropdown(db, Constants.ObjectType.Country, ddlCountries, false, string.Empty);
            }
        }

        protected override void DTOToUI(RefereeDTO dtoObj)
        {
            if (dtoObj.Referee_Id > 0)
            {
                tbFirstName.Text = dtoObj.FirstName;
                tbLastName.Text = dtoObj.LastName;
                tbDOB.Text = FormatDate(dtoObj.DOB);
                ddlCountries.SelectedValue = dtoObj.Country_Id.ToString();
                tbFirstNameEN.Text = dtoObj.FirstName_EN;
                tbLastNameEN.Text = dtoObj.LastName_EN;
            }
        }

        protected override RefereeDTO UIToDTO()
        {
            RefereeDTO RefereeToSave = new RefereeDTO()
            {
                Country_Id = int.Parse(ddlCountries.SelectedValue),
                FirstName = tbFirstName.Text.Trim(),
                LastName = tbLastName.Text.Trim(),
                Referee_Id = DataItem.Referee_Id,
                FirstName_EN = tbFirstNameEN.Text.Trim(),
                LastName_EN = tbLastNameEN.Text.Trim()
            };

            DateTime DOB = DateTime.Now;
            if (DateTime.TryParse(tbDOB.Text, out DOB))
            {
                RefereeToSave.DOB = DOB;
            }
            return RefereeToSave;
        }
    } 
}