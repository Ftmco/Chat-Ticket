using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChTi.DataBase.Context.Migrations
{
    public partial class ChatBase2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatBase",
                table: "ChatBase");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ChatBase");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "ChatBase");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ChatBase");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ChatBase");

            migrationBuilder.RenameTable(
                name: "ChatBase",
                newName: "PvChat");

            migrationBuilder.AlterColumn<Guid>(
                name: "StarterUserId",
                table: "PvChat",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OppsiteUserId",
                table: "PvChat",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PvChat",
                table: "PvChat",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Chat",
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
                    table.PrimaryKey("PK_Chat", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PvChat",
                table: "PvChat");

            migrationBuilder.RenameTable(
                name: "PvChat",
                newName: "ChatBase");

            migrationBuilder.AlterColumn<Guid>(
                name: "StarterUserId",
                table: "ChatBase",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "OppsiteUserId",
                table: "ChatBase",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ChatBase",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "ChatBase",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ChatBase",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Type",
                table: "ChatBase",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatBase",
                table: "ChatBase",
                column: "Id");
        }
    }
}
