using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;

namespace UaFootball.WebApplication
{
    public partial class MatchList : ObjectListPageBase<MatchDTO>
    {
        protected override string EditPage
        {
            get { return Constants.Pages.Edit_Match; }
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