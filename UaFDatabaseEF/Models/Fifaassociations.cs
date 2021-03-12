using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class Fifaassociations
    {
        public Fifaassociations()
        {
            Countries = new HashSet<Countries>();
        }

        public int FifaassociationId { get; set; }
        public string FifaassociationName { get; set; }
        public string FifaassociationDescription { get; set; }

        public ICollection<Countries> Countries { get; set; }
    }
}
