using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class Referees
    {
        public Referees()
        {
            Matches = new HashSet<Matches>();
        }

        public int RefereeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CountryId { get; set; }
        public DateTime? Dob { get; set; }
        public string FirstNameEn { get; set; }
        public string LastNameEn { get; set; }

        public Countries Country { get; set; }
        public ICollection<Matches> Matches { get; set; }
    }
}
