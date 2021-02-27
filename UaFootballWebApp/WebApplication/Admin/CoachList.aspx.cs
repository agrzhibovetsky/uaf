using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;

namespace UaFootball.WebApplication
{

    public partial class CoachList : ObjectListPageBase<CoachDTO>
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            List<CoachDTO> coaches = dgData.DataSource as List<CoachDTO>;
            if (coaches != null)
            {
                ddlCountry.DataSource = coaches.OrderBy(r => r.CountryName).Select(r => r.CountryName).Distinct();
                ddlCountry.DataBind();
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<CoachDTO> coaches = DTOHelper.GetAllFromDB();
            dgData.DataSource = coaches.Where(r => r.CountryName == ddlCountry.SelectedValue);
            dgData.DataBind();
        }

        protected override string EditPage
        {
            get { return Constants.Pages.Edit_Coach; }
        }

        protected override DataGrid dGrid
        {
            get
            {
                return dgData;
            }
        }
    }
}