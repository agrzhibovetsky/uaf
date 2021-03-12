using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class MultimediaTags
    {
        public int MultimediaTagId { get; set; }
        public int MultimediaId { get; set; }
        public int? PlayerId { get; set; }
        public int? MatchId { get; set; }
        public int? MatchEventId { get; set; }
        public int? ClubId { get; set; }
        public int? NationalTeamId { get; set; }

        public Clubs Club { get; set; }
        public Matches Match { get; set; }
        public MatchEvents MatchEvent { get; set; }
        public Multimedia Multimedia { get; set; }
        public NationalTeams NationalTeam { get; set; }
        public Players Player { get; set; }
    }
}
