using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UaFDatabaseEF.Models
{
    public partial class CompetitionTypes
    {
        public CompetitionTypes()
        {
            Matches = new HashSet<Matches>();
        }

        [Key]
        [Column("CompetitionType_Id")]
        public int CompetitionTypeId { get; set; }

        [MaxLength(50)]
        [Required]
        [Column("CompetitionType_Name")]
        public string CompetitionTypeName { get; set; }

        [MaxLength(1)]
        [Required]
        [Column("CompetitionTypeLevel_Cd")]
        public string CompetitionTypeLevelCd { get; set; }

        [MaxLength(10)]
        [Required]
        [Column("CompetitionType_Cd")]
        public string CompetitionTypeCd { get; set; }

        public ICollection<Matches> Matches { get; set; }
    }
}
