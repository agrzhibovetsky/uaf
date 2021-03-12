using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class Stadiums
    {
        public Stadiums()
        {
            Matches = new HashSet<Matches>();
        }

        public int StadiumId { get; set; }
        public string StadiumName { get; set; }
        public int Capacity { get; set; }
        public int CityId { get; set; }
        public int YearBuilt { get; set; }
        public string Comments { get; set; }
        public DateTime? DateAdded { get; set; }

        public Cities City { get; set; }
        public ICollection<Matches> Matches { get; set; }
    }
}
