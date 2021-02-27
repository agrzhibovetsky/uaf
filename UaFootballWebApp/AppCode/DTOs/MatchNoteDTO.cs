using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UaFootball.AppCode
{
    [Serializable]
    public class MatchNoteDTO
    {
        public int MatchNote_Id { get; set; }

        public int Match_Id { get; set; }

        public int RowIndex { get; set; }

        public string Code { get; set; }

        public string CodeDescription { get; set; }

        public string Text { get; set; }

        public MatchNoteDTO()
        {

        }
    }
}