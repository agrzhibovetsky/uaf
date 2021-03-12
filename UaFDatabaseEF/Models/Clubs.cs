using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class Clubs
    {
        public Clubs()
        {
            MatchesAwayClub = new HashSet<Matches>();
            MatchesHomeClub = new HashSet<Matches>();
            MultimediaTags = new HashSet<MultimediaTags>();
        }

        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public string DisplayName { get; set; }
        public string Logo { get; set; }
        public int? YearFound { get; set; }
        public int CityId { get; set; }

        public Cities City { get; set; }
        public ICollection<Matches> MatchesAwayClub { get; set; }
        public ICollection<Matches> MatchesHomeClub { get; set; }
        public ICollection<MultimediaTags> MultimediaTags { get; set; }
    }
}
