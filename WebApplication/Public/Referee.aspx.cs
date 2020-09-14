using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;

namespace UaFootball.WebApplication.Public
{
    public partial class Referee : UaFootballPageBase
    {

        protected RefereeDTO DataItem
        {
            get
            {
                if (ViewState["DataItem"] != null)
                    return ViewState["DataItem"] as RefereeDTO;
                else return new RefereeDTO();
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
                    int refereeId = int.Parse(Request[Constants.QueryParam.ObjectId]);
                    DataItem = new RefereeDTOHelper().GetFromDB(refereeId);

                    SearchParameters.Match searchParam = new SearchParameters.Match();
                    searchParam.Referee_Id = refereeId;
                    ml.DataBind(searchParam);
                }
            }
        }
    }
}