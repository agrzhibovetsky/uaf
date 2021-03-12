using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class Coaches
    {
        public Coaches()
        {
            MatchLineups = new HashSet<MatchLineups>();
        }

        public int CoachId { get; set; }
        public int CountryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FirstNameInt { get; set; }
        public string LastNameInt { get; set; }
        public DateTime Dob { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Countries Country { get; set; }
        public ICollection<MatchLineups> MatchLineups { get; set; }
    }
}
