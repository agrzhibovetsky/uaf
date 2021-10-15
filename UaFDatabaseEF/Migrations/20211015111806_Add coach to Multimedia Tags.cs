using Microsoft.EntityFrameworkCore.Migrations;

namespace UaFDatabaseEF.Migrations
{
    public partial class AddcoachtoMultimediaTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoachId",
                table: "MultimediaTags",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MultimediaTags_CoachId",
                table: "MultimediaTags",
                column: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_MultimediaTags_Coaches_CoachId",
                table: "MultimediaTags",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "CoachId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultimediaTags_Coaches_CoachId",
                table: "MultimediaTags");

            migrationBuilder.DropIndex(
                name: "IX_MultimediaTags_CoachId",
                table: "MultimediaTags");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "MultimediaTags");
        }
    }
}
