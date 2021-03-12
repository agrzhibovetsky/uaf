using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class MatchLineups
    {
        public int MatchLineupId { get; set; }
        public int? PlayerId { get; set; }
        public int? ShirtNumber { get; set; }
        public bool IsHomeTeamPlayer { get; set; }
        public bool IsSubstitute { get; set; }
        public int MatchId { get; set; }
        public int? CoachId { get; set; }
        public int? Flags { get; set; }

        public Coaches Coach { get; set; }
        public Matches Match { get; set; }
        public Players Player { get; set; }
    }
}
