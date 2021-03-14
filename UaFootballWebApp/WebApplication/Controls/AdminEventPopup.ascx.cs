using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;

namespace UaFootball.WebApplication.Controls
{
    public partial class AdminEventPopup : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Dictionary<int, string> goalEventFlags = UIHelper.EventCodeEventFlagsMap[Constants.DB.EventTypeCodes.Goal];
                int[] goalFlags1 = { Constants.DB.EventFlags.LeftLeg, Constants.DB.EventFlags.RightLeg, Constants.DB.EventFlags.Head, Constants.DB.EventFlags.OtherBodyPart };
                int[] goalFlags2 = { Constants.DB.EventFlags.GoalClass1, Constants.DB.EventFlags.GoalClass2, Constants.DB.EventFlags.GoalClass3, Constants.DB.EventFlags.GoalClass4 };
                int[] goalFlags3 = { Constants.DB.EventFlags.LongDistance, Constants.DB.EventFlags.MiddleDistance, Constants.DB.EventFlags.ShortDistance};
                
                cblGoalFlags1.DataSource = goalEventFlags.Where(f=> goalFlags1.Contains(f.Key));
                cblGoalFlags1.DataTextField = "Value";
                cblGoalFlags1.DataValueField = "Key";
                cblGoalFlags1.DataBind();

                cblGoalFlags2.DataSource = goalEventFlags.Where(f => goalFlags2.Contains(f.Key));
                cblGoalFlags2.DataTextField = "Value";
                cblGoalFlags2.DataValueField = "Key";
                cblGoalFlags2.DataBind();

                cblGoalFlags3.DataSource = goalEventFlags.Where(f => goalFlags3.Contains(f.Key));
                cblGoalFlags3.DataTextField = "Value";
                cblGoalFlags3.DataValueField = "Key";
                cblGoalFlags3.DataBind();

                cblGoalFlags4.DataSource = goalEventFlags.Where(f => !goalFlags1.Contains(f.Key) && !goalFlags2.Contains(f.Key) && !goalFlags3.Contains(f.Key));
                cblGoalFlags4.DataTextField = "Value";
                cblGoalFlags4.DataValueField = "Key";
                cblGoalFlags4.DataBind();

                Dictionary<int, string> penaltyEventFlags = UIHelper.EventCodeEventFlagsMap[Constants.DB.EventTypeCodes.Penalty];
                cblPenaltyFlags.DataSource = penaltyEventFlags;
                cblPenaltyFlags.DataTextField = "Value";
                cblPenaltyFlags.DataValueField = "Key";
                cblPenaltyFlags.DataBind();
            }
        }
    }
}