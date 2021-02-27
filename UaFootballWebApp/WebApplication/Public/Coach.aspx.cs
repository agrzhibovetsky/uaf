using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;


namespace UaFootball.WebApplication.Public
{
    public partial class Coach : UaFootballPageBase
    {
        protected CoachDTO DataItem
        {
            get
            {
                if (ViewState["DataItem"] != null)
                    return ViewState["DataItem"] as CoachDTO;
                else return new CoachDTO();
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
                    int coachId = int.Parse(Request[Constants.QueryParam.ObjectId]);
                    DataItem = new CoachDTOHelper().GetFromDB(coachId);

                    SearchParameters.Match searchParam = new SearchParameters.Match();
                    searchParam.Coach_Id = coachId;
                    ml.DataBind(searchParam);
                }
            }
        }
    }
    
}