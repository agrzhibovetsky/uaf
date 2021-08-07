using Microsoft.EntityFrameworkCore.Migrations;

namespace UaFDatabaseEF.Migrations
{
    public partial class MakePlayer1Idnulable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Player1_Id",
                table: "MatchEvents",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Player1_Id",
                table: "MatchEvents",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
