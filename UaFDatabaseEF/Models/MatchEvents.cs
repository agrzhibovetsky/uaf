using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class MatchEvents
    {
        public MatchEvents()
        {
            MultimediaTags = new HashSet<MultimediaTags>();
        }

        public int MatchEventId { get; set; }
        public string EventCd { get; set; }
        public int Minute { get; set; }
        public int Player1Id { get; set; }
        public int? Player2Id { get; set; }
        public int MatchId { get; set; }
        public long? EventFlags { get; set; }

        public Matches Match { get; set; }
        public Players Player1 { get; set; }
        public Players Player2 { get; set; }
        public ICollection<MultimediaTags> MultimediaTags { get; set; }
    }
}
