using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChTi.DataBase.Context.Migrations.Ticket
{
    public partial class TicketCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Ticket",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Ticket",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Ticket");
        }
    }
}
