using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChTi.DataBase.Context.Migrations
{
    public partial class ChatBaseInhretance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "PvChat");

            migrationBuilder.AlterColumn<short>(
                name: "Type",
                table: "ChatBase",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ChatBase",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "ChatBase",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "OppsiteUserId",
                table: "ChatBase",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StarterUserId",
                table: "ChatBase",
                type: "uuid",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "ChatBase");

            migrationBuilder.DropColumn(
                name: "OppsiteUserId",
                table: "ChatBase");

            migrationBuilder.DropColumn(
                name: "StarterUserId",
                table: "ChatBase");

            migrationBuilder.AlterColumn<short>(
                name: "Type",
                table: "ChatBase",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ChatBase",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatBaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_ChatBase_ChatBaseId",
                        column: x => x.ChatBaseId,
                        principalTable: "ChatBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PvChat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatBaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    OppsiteUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StarterUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PvChat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PvChat_ChatBase_ChatBaseId",
                        column: x => x.ChatBaseId,
                        principalTable: "ChatBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chat_ChatBaseId",
                table: "Chat",
                column: "ChatBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PvChat_ChatBaseId",
                table: "PvChat",
                column: "ChatBaseId");
        }
    }
}
