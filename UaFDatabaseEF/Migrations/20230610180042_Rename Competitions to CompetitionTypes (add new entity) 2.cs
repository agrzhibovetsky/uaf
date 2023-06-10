using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UaFDatabaseEF.Migrations
{
    public partial class RenameCompetitionstoCompetitionTypesaddnewentity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompetitionTypeId",
                table: "Matches",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompetitionTypes",
                columns: table => new
                {
                    CompetitionTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompetitionTypeName = table.Column<string>(maxLength: 50, nullable: false),
                    CompetitionTypeLevelCd = table.Column<string>(maxLength: 1, nullable: false),
                    CompetitionTypeCd = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionTypes", x => x.CompetitionTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_CompetitionTypeId",
                table: "Matches",
                column: "CompetitionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_CompetitionTypes_CompetitionTypeId",
                table: "Matches",
                column: "CompetitionTypeId",
                principalTable: "CompetitionTypes",
                principalColumn: "CompetitionTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_CompetitionTypes_CompetitionTypeId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "CompetitionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Matches_CompetitionTypeId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "CompetitionTypeId",
                table: "Matches");
        }
    }
}
