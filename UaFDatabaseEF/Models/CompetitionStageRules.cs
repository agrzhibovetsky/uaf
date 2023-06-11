using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UaFDatabaseEF.Models
{
    public class CompetitionStageRules
    {
        public CompetitionStageRules()
        {
           
        }

        [Key]
        [Column("CompetitionStageRule_Id")]
        public int CompetitionStageRuleId { get; set; }

        public string Description { get; set; }
    }
}
