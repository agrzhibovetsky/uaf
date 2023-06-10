using Microsoft.EntityFrameworkCore.Migrations;

namespace UaFDatabaseEF.Migrations
{
    public partial class RenameCompetitionstoCompetitionTypesaddnewentity3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompetitionTypeName",
                table: "CompetitionTypes",
                newName: "CompetitionType_Name");

            migrationBuilder.RenameColumn(
                name: "CompetitionTypeLevelCd",
                table: "CompetitionTypes",
                newName: "CompetitionTypeLevel_Cd");

            migrationBuilder.RenameColumn(
                name: "CompetitionTypeCd",
                table: "CompetitionTypes",
                newName: "CompetitionType_Cd");

            migrationBuilder.RenameColumn(
                name: "CompetitionTypeId",
                table: "CompetitionTypes",
                newName: "CompetitionType_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompetitionType_Name",
                table: "CompetitionTypes",
                newName: "CompetitionTypeName");

            migrationBuilder.RenameColumn(
                name: "CompetitionTypeLevel_Cd",
                table: "CompetitionTypes",
                newName: "CompetitionTypeLevelCd");

            migrationBuilder.RenameColumn(
                name: "CompetitionType_Cd",
                table: "CompetitionTypes",
                newName: "CompetitionTypeCd");

            migrationBuilder.RenameColumn(
                name: "CompetitionType_Id",
                table: "CompetitionTypes",
                newName: "CompetitionTypeId");
        }
    }
}
