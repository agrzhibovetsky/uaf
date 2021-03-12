using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class NationalTeams
    {
        public NationalTeams()
        {
            MatchesAwayNationalTeam = new HashSet<Matches>();
            MatchesHomeNationalTeam = new HashSet<Matches>();
            MultimediaTags = new HashSet<MultimediaTags>();
        }

        public int NationalTeamId { get; set; }
        public int CountryId { get; set; }
        public string Logo { get; set; }
        public string NationalTeamTypeCd { get; set; }

        public Countries Country { get; set; }
        public ICollection<Matches> MatchesAwayNationalTeam { get; set; }
        public ICollection<Matches> MatchesHomeNationalTeam { get; set; }
        public ICollection<MultimediaTags> MultimediaTags { get; set; }
    }
}
