using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;

namespace UaFootball.WebApplication.Controls
{
    public partial class MatchList : UserControl
    {
        public string ExtraColumn { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ExtraColumn) && ExtraColumn.Equals("Spectators"))
            {
                lblExtraColumnName.Text = "Зрители";
            }
        }

        public void DataBind(SearchParameters.Match searchParam)
        {
            rptGames.DataSource = new MatchDTOHelper().GetListFromDB(searchParam);
            rptGames.DataBind();
        }

        protected void rptGames_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
            if (!string.IsNullOrEmpty(ExtraColumn))
            {
                Label lblExtraColumnText = e.Item.FindControl("lblExtraColumnText") as Label;
                if (ExtraColumn.Equals("Spectators"))
                {
                    lblExtraColumnText.Text = (e.Item.DataItem as MatchDTO).Spectators.ToString();
                }
            }
        }
        
    }
}