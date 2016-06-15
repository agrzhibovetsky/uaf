using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;

namespace UaFootball.WebApplication
{
    public partial class CountryList : ObjectListPageBase<CountryDTO>
    {
        protected override string EditPage
        {
            get { return Constants.Pages.Edit_Country; }
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