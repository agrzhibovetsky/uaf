using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using UaFootball.AppCode;
using UaFDatabase;

namespace UaFootball.WebApplication
{
    public partial class ClubEdit : ObjectEditPageBase<ClubDTO>
    {
        protected override string ObjectListPage
        {
            get { return Constants.Pages.List_Club; }
        }

        protected override void PrepareUI()
        {
            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                BindDropdown(db, Constants.ObjectType.City, ddlCities, false, string.Empty);
            }
        }

        protected string LogoPath
        {
            get
            {
                return ResolveClientUrl(Constants.Paths.DropBoxWebPath);
            }
        }

        protected override void DTOToUI(ClubDTO dtoObj)
        {
            if (dtoObj.Club_ID > 0)
            {
                tbName.Text = dtoObj.Club_Name;
                tbDisplayName.Text = dtoObj.Display_Name;
                tbYearFound.Text = dtoObj.Year_Found.HasValue ? dtoObj.Year_Found.Value.ToString() : string.Empty;
                ddlCities.SelectedValue = dtoObj.City_ID.ToString();
                if (dtoObj.Logo!=null)
                {
                    imgLogo.ImageUrl = PathHelper.GetWebPath(this, Constants.Paths.MutlimediaWebRoot, dtoObj.Logo.FilePath, dtoObj.Logo.FileName);
                }
            }
        }

        protected override ClubDTO UIToDTO()
        {
            ClubDTO ClubToSave = new ClubDTO()
            {
                Club_Name = tbName.Text,
                Club_ID = DataItem.Club_ID,
                Year_Found = tbYearFound.Text.ParseInt(false),
                //Logo = tbLogo.Text.IsEmpty() ? null : tbLogo.Text,
                Display_Name = tbDisplayName.Text.IsEmpty() ? null : tbDisplayName.Text,
                City_ID = int.Parse(ddlCities.SelectedValue)
            };


            return ClubToSave;
        }
    } 
}