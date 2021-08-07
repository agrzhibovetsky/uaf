using Microsoft.EntityFrameworkCore.Migrations;

namespace UaFDatabaseEF.Migrations
{
    public partial class Addcoachtomatcheventscorrectadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoachId",
                table: "MatchEvents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchEvents_CoachId",
                table: "MatchEvents",
                column: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchEvents_Coaches_CoachId",
                table: "MatchEvents",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "CoachId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchEvents_Coaches_CoachId",
                table: "MatchEvents");

            migrationBuilder.DropIndex(
                name: "IX_MatchEvents_CoachId",
                table: "MatchEvents");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "MatchEvents");
        }
    }
}
