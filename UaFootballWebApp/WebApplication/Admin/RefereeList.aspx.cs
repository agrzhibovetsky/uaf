using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;


namespace UaFootball.WebApplication
{
    public partial class RefereeList : ObjectListPageBase<RefereeDTO>
    {

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            List<RefereeDTO> referees = dgData.DataSource as List<RefereeDTO>;
            if (referees != null)
            {
                ddlCountry.DataSource = referees.OrderBy(r => r.CountryName).Select(r => r.CountryName).Distinct();
                ddlCountry.DataBind();
            }
        }

        protected override string EditPage
        {
            get { return Constants.Pages.Edit_Referee; }
        }

        protected override DataGrid dGrid
        {
            get
            {
                return dgData;
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<RefereeDTO> referees = DTOHelper.GetAllFromDB();
            dgData.DataSource = referees.Where(r => r.CountryName == ddlCountry.SelectedValue);
            dgData.DataBind();
        }
    } 
}