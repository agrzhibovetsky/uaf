using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class Competitions
    {
        public Competitions()
        {
            Matches = new HashSet<Matches>();
        }

        public int CompetitionId { get; set; }
        public string CompetitionName { get; set; }
        public string CompetitionLevelCd { get; set; }
        public string CompetitionCd { get; set; }

        public ICollection<Matches> Matches { get; set; }
    }
}
