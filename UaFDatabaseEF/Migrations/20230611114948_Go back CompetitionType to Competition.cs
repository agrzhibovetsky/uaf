using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UaFDatabaseEF.Migrations
{
    public partial class GobackCompetitionTypetoCompetition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionEditions_CompetitionTypes_CompetitionType_Id",
                table: "CompetitionEditions");

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

            migrationBuilder.RenameColumn(
                name: "CompetitionType_Id",
                table: "CompetitionEditions",
                newName: "Competition_Id");

            migrationBuilder.RenameIndex(
                name: "IX_CompetitionEditions_CompetitionType_Id",
                table: "CompetitionEditions",
                newName: "IX_CompetitionEditions_Competition_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionEditions_Competitions_Competition_Id",
                table: "CompetitionEditions",
                column: "Competition_Id",
                principalTable: "Competitions",
                principalColumn: "Competition_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionEditions_Competitions_Competition_Id",
                table: "CompetitionEditions");

            migrationBuilder.RenameColumn(
                name: "Competition_Id",
                table: "CompetitionEditions",
                newName: "CompetitionType_Id");

            migrationBuilder.RenameIndex(
                name: "IX_CompetitionEditions_Competition_Id",
                table: "CompetitionEditions",
                newName: "IX_CompetitionEditions_CompetitionType_Id");

            migrationBuilder.AddColumn<int>(
                name: "CompetitionTypeId",
                table: "Matches",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompetitionTypes",
                columns: table => new
                {
                    CompetitionType_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompetitionType_Cd = table.Column<string>(maxLength: 10, nullable: false),
                    CompetitionTypeLevel_Cd = table.Column<string>(maxLength: 1, nullable: false),
                    CompetitionType_Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionTypes", x => x.CompetitionType_Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_CompetitionTypeId",
                table: "Matches",
                column: "CompetitionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionEditions_CompetitionTypes_CompetitionType_Id",
                table: "CompetitionEditions",
                column: "CompetitionType_Id",
                principalTable: "CompetitionTypes",
                principalColumn: "CompetitionType_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_CompetitionTypes_CompetitionTypeId",
                table: "Matches",
                column: "CompetitionTypeId",
                principalTable: "CompetitionTypes",
                principalColumn: "CompetitionType_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
