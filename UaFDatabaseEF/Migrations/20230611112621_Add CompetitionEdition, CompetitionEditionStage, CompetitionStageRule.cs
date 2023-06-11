using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UaFDatabaseEF.Migrations
{
    public partial class AddCompetitionEditionCompetitionEditionStageCompetitionStageRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompetitionEdition_Id",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompetitionEditionStage_Id",
                table: "Matches",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompetitionEditions",
                columns: table => new
                {
                    CompetitionEdition_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompetitionType_Id = table.Column<int>(nullable: false),
                    CompetitionSeason_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionEditions", x => x.CompetitionEdition_Id);
                    table.ForeignKey(
                        name: "FK_CompetitionEditions_Seasons_CompetitionSeason_Id",
                        column: x => x.CompetitionSeason_Id,
                        principalTable: "Seasons",
                        principalColumn: "Season_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionEditions_CompetitionTypes_CompetitionType_Id",
                        column: x => x.CompetitionType_Id,
                        principalTable: "CompetitionTypes",
                        principalColumn: "CompetitionType_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionStageRules",
                columns: table => new
                {
                    CompetitionStageRule_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionStageRules", x => x.CompetitionStageRule_Id);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionEditionStages",
                columns: table => new
                {
                    CompetitionEditionStageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompetitionEdition_Id = table.Column<int>(nullable: false),
                    CompetitionStage_Id = table.Column<int>(nullable: false),
                    CompetitionStage_Branch = table.Column<int>(nullable: false),
                    CompetitionStage_Order = table.Column<int>(nullable: false),
                    CompetitionEditionStage_Kind = table.Column<string>(maxLength: 5, nullable: true),
                    CompetitionStageRule_Id = table.Column<int>(nullable: false),
                    CompetitionStageRuleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionEditionStages", x => x.CompetitionEditionStageId);
                    table.ForeignKey(
                        name: "FK_CompetitionEditionStages_CompetitionEditions_CompetitionEdition_Id",
                        column: x => x.CompetitionEdition_Id,
                        principalTable: "CompetitionEditions",
                        principalColumn: "CompetitionEdition_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionEditionStages_CompetitionStages_CompetitionStage_Id",
                        column: x => x.CompetitionStage_Id,
                        principalTable: "CompetitionStages",
                        principalColumn: "CompetitionStage_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionEditionStages_CompetitionStageRules_CompetitionStageRuleId",
                        column: x => x.CompetitionStageRuleId,
                        principalTable: "CompetitionStageRules",
                        principalColumn: "CompetitionStageRule_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_CompetitionEdition_Id",
                table: "Matches",
                column: "CompetitionEdition_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_CompetitionEditionStage_Id",
                table: "Matches",
                column: "CompetitionEditionStage_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEditions_CompetitionSeason_Id",
                table: "CompetitionEditions",
                column: "CompetitionSeason_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEditions_CompetitionType_Id",
                table: "CompetitionEditions",
                column: "CompetitionType_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEditionStages_CompetitionEdition_Id",
                table: "CompetitionEditionStages",
                column: "CompetitionEdition_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEditionStages_CompetitionStage_Id",
                table: "CompetitionEditionStages",
                column: "CompetitionStage_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEditionStages_CompetitionStageRuleId",
                table: "CompetitionEditionStages",
                column: "CompetitionStageRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_CompetitionEditions_CompetitionEdition_Id",
                table: "Matches",
                column: "CompetitionEdition_Id",
                principalTable: "CompetitionEditions",
                principalColumn: "CompetitionEdition_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_CompetitionEditionStages_CompetitionEditionStage_Id",
                table: "Matches",
                column: "CompetitionEditionStage_Id",
                principalTable: "CompetitionEditionStages",
                principalColumn: "CompetitionEditionStageId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_CompetitionEditions_CompetitionEdition_Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_CompetitionEditionStages_CompetitionEditionStage_Id",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "CompetitionEditionStages");

            migrationBuilder.DropTable(
                name: "CompetitionEditions");

            migrationBuilder.DropTable(
                name: "CompetitionStageRules");

            migrationBuilder.DropIndex(
                name: "IX_Matches_CompetitionEdition_Id",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_CompetitionEditionStage_Id",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "CompetitionEdition_Id",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "CompetitionEditionStage_Id",
                table: "Matches");
        }
    }
}
