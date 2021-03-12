using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class Cities
    {
        public Cities()
        {
            Clubs = new HashSet<Clubs>();
            Stadiums = new HashSet<Stadiums>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }

        public Countries Country { get; set; }
        public ICollection<Clubs> Clubs { get; set; }
        public ICollection<Stadiums> Stadiums { get; set; }
    }
}
