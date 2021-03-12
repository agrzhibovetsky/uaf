using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class Players
    {
        public Players()
        {
            MatchEventsPlayer1 = new HashSet<MatchEvents>();
            MatchEventsPlayer2 = new HashSet<MatchEvents>();
            MatchLineups = new HashSet<MatchLineups>();
            MultimediaTags = new HashSet<MultimediaTags>();
            PlayerPositions = new HashSet<PlayerPositions>();
        }

        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string DisplayName { get; set; }
        public DateTime? Dob { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public int CountryId { get; set; }
        public string FirstNameInt { get; set; }
        public string LastNameInt { get; set; }
        public bool? RequiresReview { get; set; }
        public string UacityName { get; set; }
        public string UaregionName { get; set; }
        public DateTime? LastUpdateDt { get; set; }
        public string NameSearchString { get; set; }

        public Countries Country { get; set; }
        public ICollection<MatchEvents> MatchEventsPlayer1 { get; set; }
        public ICollection<MatchEvents> MatchEventsPlayer2 { get; set; }
        public ICollection<MatchLineups> MatchLineups { get; set; }
        public ICollection<MultimediaTags> MultimediaTags { get; set; }
        public ICollection<PlayerPositions> PlayerPositions { get; set; }
    }
}
