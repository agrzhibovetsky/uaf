using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using UaFootball.AppCode;

namespace UaFootball.WebApplication
{
    public partial class AutocompleteTextBox : System.Web.UI.UserControl
    {
        private string _behaviorId;
        private AutocompleteType _autocompleteType;
        private bool _isRequired;

        public AutocompleteType AutocompleteType
        {
            get { return _autocompleteType; }
            set { _autocompleteType = value; }
        }

        public string BehaviorId
        {
            get { return _behaviorId; }
            set { _behaviorId = value; }
        }

        public string Text
        {
            get { return tb.Text; }
            set { tb.Text = value; }
        }

        public string Value
        {
            get { return hf.Value; }
            set { hf.Value = value; }
        }

        public bool IsRequired
        {
            get { return _isRequired; }
            set { _isRequired = value; }
        }

        public string Placeholder
        {
            get;
            set;
        }


        protected override void OnInit(EventArgs e)
        {
            tb.ID = "tb" + BehaviorId;
            hf.ID = "hf" + BehaviorId;
            if (!string.IsNullOrEmpty(Placeholder))
            {
                tb.Attributes.Add("placeholder", Placeholder);
            }
            try
            {
                Page.ClientScript.RegisterExpandoAttribute(cvAutocomplete.ClientID, "valueHolderId", hf.ClientID);
                Page.ClientScript.RegisterExpandoAttribute(cvAutocomplete.ClientID, "textHolderId", tb.ClientID);
            }
            catch (Exception ex)
            {

            }
            cvAutocomplete.Enabled = IsRequired;
            base.OnInit(e);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterClientScriptInclude(Constants.Paths.AutocompleteKey, Page.ResolveClientUrl(Constants.Paths.AutocompletePath));

            string clientObjName = BehaviorId;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine("var " + clientObjName + " = new autocompleteTextBox()");
            sb.AppendLine(clientObjName + ".setAutocompleteType(\"" + AutocompleteType.ToString() + "\");");
            sb.AppendLine(clientObjName + ".setAutocompletePath(\"" + Page.ResolveUrl(Constants.Pages.Autocomplete) + "\");");
            sb.AppendLine(clientObjName + ".setTextBoxId(\"" + tb.ClientID + "\");");
            sb.AppendLine(clientObjName + ".setHiddenFiedlId(\"" + hf.ClientID + "\");");
            sb.AppendLine("$(document).ready(function () {");
            sb.AppendLine(clientObjName + ".init(); });");
            sb.Append("</script>");
            cs.RegisterClientScriptBlock(this.GetType(), this.UniqueID, sb.ToString());
        }
    } 
}