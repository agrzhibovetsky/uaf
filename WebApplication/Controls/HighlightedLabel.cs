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
                string[] wordsToHighlight = TextToHighlight.Trim().Split(' ');
                string[] allWords = Text.Split(' ');
                foreach (string s in allWords)
                {
                    //string normalizedS = s.Replace(",","");

                    bool highlight = false;
                    int highlightStartIndex = s.Length;
                    int highlightEndIndex = 0;

                    foreach (string strToHighlight in wordsToHighlight)
                    {
                        if (s.ToUpper().Contains(strToHighlight.ToUpper()))
                        {
                            highlight = true;
                            highlightStartIndex = Math.Min(highlightStartIndex, s.ToUpper().IndexOf(strToHighlight.ToUpper()));
                            highlightEndIndex = Math.Max(highlightEndIndex, s.ToUpper().IndexOf(strToHighlight.ToUpper())+strToHighlight.Length);
                        }
                    }

                    if (highlight)
                    {
                        string strBefore = s.Substring(0, highlightStartIndex);
                        string strMiddle = s.Substring(highlightStartIndex, highlightEndIndex - highlightStartIndex);
                        string strAfter = s.Substring(highlightEndIndex);
                        writer.Write(string.Format("{2}<span class='{0}'>{1}</span>{3}", CssClassForHighlight, strMiddle, strBefore, strAfter));
                    }
                    else
                    {
                        writer.Write(s + " ");
                    }
                    
                }
            }
        }
    }
}