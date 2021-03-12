using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class MatchNotes
    {
        public int MatchNoteId { get; set; }
        public int MatchId { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }

        public Matches Match { get; set; }
    }
}
