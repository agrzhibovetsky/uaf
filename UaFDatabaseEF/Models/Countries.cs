using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class Countries
    {
        public Countries()
        {
            Cities = new HashSet<Cities>();
            Coaches = new HashSet<Coaches>();
            NationalTeams = new HashSet<NationalTeams>();
            Players = new HashSet<Players>();
            Referees = new HashSet<Referees>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public int FifaassociationId { get; set; }

        public Fifaassociations Fifaassociation { get; set; }
        public ICollection<Cities> Cities { get; set; }
        public ICollection<Coaches> Coaches { get; set; }
        public ICollection<NationalTeams> NationalTeams { get; set; }
        public ICollection<Players> Players { get; set; }
        public ICollection<Referees> Referees { get; set; }
    }
}
