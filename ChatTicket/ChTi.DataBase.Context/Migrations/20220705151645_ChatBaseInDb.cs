using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChTi.DataBase.Context.Migrations
{
    public partial class ChatBaseInDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Chat");

            migrationBuilder.AddColumn<Guid>(
                name: "ChatBaseId",
                table: "Chat",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ChatBase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PvChat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatBaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    StarterUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    OppsiteUserId = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_ChatBase_ChatBaseId",
                table: "Chat",
                column: "ChatBaseId",
                principalTable: "ChatBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_ChatBase_ChatBaseId",
                table: "Chat");

            migrationBuilder.DropTable(
                name: "PvChat");

            migrationBuilder.DropTable(
                name: "ChatBase");

            migrationBuilder.DropIndex(
                name: "IX_Chat_ChatBaseId",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "ChatBaseId",
                table: "Chat");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Chat",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<short>(
                name: "Status",
                table: "Chat",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Chat",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "Type",
                table: "Chat",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Chat",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
