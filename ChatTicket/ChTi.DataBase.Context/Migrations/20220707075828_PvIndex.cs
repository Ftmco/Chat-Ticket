using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChTi.DataBase.Context.Migrations
{
    public partial class PvIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PvChat_StarterUserId_OppsiteUserId",
                table: "PvChat",
                columns: new[] { "StarterUserId", "OppsiteUserId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PvChat_StarterUserId_OppsiteUserId",
                table: "PvChat");
        }
    }
}
