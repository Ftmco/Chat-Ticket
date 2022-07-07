using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChTi.DataBase.Context.Migrations
{
    public partial class ChannelIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.CreateTable(
                name: "ChannelChat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Link = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelChat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupChat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChat", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PvChat_Token",
                table: "PvChat",
                column: "Token");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelChat_Link",
                table: "ChannelChat",
                column: "Link");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelChat_Token",
                table: "ChannelChat",
                column: "Token");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChat_Token",
                table: "GroupChat",
                column: "Token");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelChat");

            migrationBuilder.DropTable(
                name: "GroupChat");

            migrationBuilder.DropIndex(
                name: "IX_PvChat_Token",
                table: "PvChat");

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                });
        }
    }
}
