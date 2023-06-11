using Microsoft.EntityFrameworkCore.Migrations;

namespace UaFDatabaseEF.Migrations
{
    public partial class Smallfixesforstagerules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionEditionStages_CompetitionStageRules_CompetitionStageRuleId",
                table: "CompetitionEditionStages");

            migrationBuilder.DropIndex(
                name: "IX_CompetitionEditionStages_CompetitionStageRuleId",
                table: "CompetitionEditionStages");

            migrationBuilder.DropColumn(
                name: "CompetitionStageRuleId",
                table: "CompetitionEditionStages");

            migrationBuilder.AddColumn<int>(
                name: "DefaultStageRule_Id",
                table: "CompetitionStages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEditionStages_CompetitionStageRule_Id",
                table: "CompetitionEditionStages",
                column: "CompetitionStageRule_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionEditionStages_CompetitionStageRules_CompetitionStageRule_Id",
                table: "CompetitionEditionStages",
                column: "CompetitionStageRule_Id",
                principalTable: "CompetitionStageRules",
                principalColumn: "CompetitionStageRule_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionEditionStages_CompetitionStageRules_CompetitionStageRule_Id",
                table: "CompetitionEditionStages");

            migrationBuilder.DropIndex(
                name: "IX_CompetitionEditionStages_CompetitionStageRule_Id",
                table: "CompetitionEditionStages");

            migrationBuilder.DropColumn(
                name: "DefaultStageRule_Id",
                table: "CompetitionStages");

            migrationBuilder.AddColumn<int>(
                name: "CompetitionStageRuleId",
                table: "CompetitionEditionStages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEditionStages_CompetitionStageRuleId",
                table: "CompetitionEditionStages",
                column: "CompetitionStageRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionEditionStages_CompetitionStageRules_CompetitionStageRuleId",
                table: "CompetitionEditionStages",
                column: "CompetitionStageRuleId",
                principalTable: "CompetitionStageRules",
                principalColumn: "CompetitionStageRule_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
