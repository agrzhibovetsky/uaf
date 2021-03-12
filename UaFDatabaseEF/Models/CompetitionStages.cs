using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class CompetitionStages
    {
        public CompetitionStages()
        {
            Matches = new HashSet<Matches>();
        }

        public int CompetitionStageId { get; set; }
        public int CompetitionId { get; set; }
        public string CompetitionStageName { get; set; }

        public ICollection<Matches> Matches { get; set; }
    }
}
