using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UaFDatabaseEF.Models
{
    public partial class CompetitionEditionStages
    {
        public CompetitionEditionStages()
        {
            Matches = new HashSet<Matches>();
        }

        [Key]
        public int CompetitionEditionStageId { get; set; }

        [Column("CompetitionEdition_Id")]
        public int CompetitionEditionId { get; set; }

        [Column("CompetitionStage_Id")]
        public int CompetitionStageId { get; set; }

        [Column("CompetitionStage_Branch")]
        public int CompetitionStageBranch { get; set; }

        [Column("CompetitionStage_Order")]
        public int CompetitionStageOrder { get; set; }

        [Column("CompetitionEditionStage_Kind")]
        [MaxLength(5)]
        public string CompetitionStageKind { get; set; }

        [Column("CompetitionStageRule_Id")]
        public int CompetitionStageRuleId { get; set; }

        public CompetitionEditions CompetitionEdition { get; set; }
        public CompetitionStages CompetitionStage { get; set; }
        public CompetitionStageRules CompetitionStageRule { get; set; }
        public ICollection<Matches> Matches { get; set; }

    }
}
