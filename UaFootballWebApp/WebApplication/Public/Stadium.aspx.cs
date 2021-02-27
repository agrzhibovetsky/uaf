using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;

namespace UaFootball.WebApplication.Public
{
    public partial class Stadium : UaFootballPageBase
    {

        protected StadiumDTO DataItem
        {
            get
            {
                if (ViewState["DataItem"] != null)
                    return ViewState["DataItem"] as StadiumDTO;
                else return new StadiumDTO();
            }
            set
            {
                ViewState["DataItem"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request[Constants.QueryParam.ObjectId].Length > 0)
                {
                    int stadiumId = int.Parse(Request[Constants.QueryParam.ObjectId]);
                    DataItem = new StadiumDTOHelper().GetFromDB(stadiumId);

                    SearchParameters.Match searchParam = new SearchParameters.Match();
                    searchParam.Stadium_Id = stadiumId;
                    ml.DataBind(searchParam);
                }
            }
        }
    }
}