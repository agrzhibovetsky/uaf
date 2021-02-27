using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;

namespace UaFootball.WebApplication
{
    public partial class ClubList : ObjectListPageBase<ClubDTO>
    {

        protected override string EditPage
        {
            get { return Constants.Pages.Edit_Club; }
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