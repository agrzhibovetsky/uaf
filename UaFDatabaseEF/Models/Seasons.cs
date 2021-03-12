using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class Seasons
    {
        public Seasons()
        {
            Matches = new HashSet<Matches>();
        }

        public int SeasonId { get; set; }
        public string SeasonDescription { get; set; }
        public string CompetitionLevelCd { get; set; }
        public string SeasonCd { get; set; }
        public DateTime? StartDate { get; set; }

        public ICollection<Matches> Matches { get; set; }
    }
}
