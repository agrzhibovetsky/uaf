using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;

namespace UaFootball.WebApplication.Controls
{
    public partial class MatchNotes : UserControl
    {
        public string NoteTypeCode { get; set; }
        public List<MatchNoteDTO> DataSource { get; set; }

        public override void DataBind()
        {
            if (DataSource != null)
            {
                rptNotes.DataSource = DataSource.Where(n=>n.Code == NoteTypeCode);
                rptNotes.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}