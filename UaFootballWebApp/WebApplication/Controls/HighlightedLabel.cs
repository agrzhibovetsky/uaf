using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace UaFootball.WebApplication.Controls
{
    public class HighlightedLabel : Label
    {
        public string TextToHighlight {get; set;}

        public string CssClassForHighlight { get; set; }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (TextToHighlight.Trim().Length > 0)
            {
                string[] wordsToHighlight = TextToHighlight.Trim().Split(' ').Where(t=>t.Length>3).ToArray();
                string html = Text;
                foreach (string wordToHighlight in wordsToHighlight)
                {
                    if (html.Contains(wordToHighlight))
                    {
                       html = html.Replace(wordToHighlight, string.Format("<span class={0}>{1}</span>", CssClassForHighlight, wordToHighlight));
                    }
                }
                writer.Write(html);
                
            }
        }
    }
}