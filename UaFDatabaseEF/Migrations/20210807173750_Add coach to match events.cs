using Microsoft.EntityFrameworkCore.Migrations;

namespace UaFDatabaseEF.Migrations
{
    public partial class Addcoachtomatchevents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CoachId",
                table: "MatchEvents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoachId1",
                table: "MatchEvents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchEvents_CoachId1",
                table: "MatchEvents",
                column: "CoachId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchEvents_Coaches_CoachId1",
                table: "MatchEvents",
                column: "CoachId1",
                principalTable: "Coaches",
                principalColumn: "CoachId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchEvents_Coaches_CoachId1",
                table: "MatchEvents");

            migrationBuilder.DropIndex(
                name: "IX_MatchEvents_CoachId1",
                table: "MatchEvents");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "MatchEvents");

            migrationBuilder.DropColumn(
                name: "CoachId1",
                table: "MatchEvents");
        }
    }
}
