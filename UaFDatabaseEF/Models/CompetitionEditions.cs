using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UaFDatabaseEF.Models
{
    public partial class CompetitionEditions
    {
        public CompetitionEditions()
        {
            Matches = new HashSet<Matches>();
        }

        [Key]
        [Column("CompetitionEdition_Id")]
        public int CompetitionEditionId { get; set; }

        [Column("Competition_Id")]
        public int CompetitionId { get; set; }

        [Column("CompetitionSeason_Id")]
        public int CompetitionSeasonId { get; set; }

        public ICollection<Matches> Matches { get; set; }

        public Seasons CompetitionSeason { get; set; }
        public Competitions Competition { get; set; }
    }
}
