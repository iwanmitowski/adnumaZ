using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace adnumaZ.Data.Migrations
{
    public partial class TorrentHash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "Torrents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 11, 28, 20, 45, 17, 333, DateTimeKind.Utc).AddTicks(3371),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 11, 22, 8, 7, 12, 375, DateTimeKind.Utc).AddTicks(8151));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hash",
                table: "Torrents");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 11, 22, 8, 7, 12, 375, DateTimeKind.Utc).AddTicks(8151),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 11, 28, 20, 45, 17, 333, DateTimeKind.Utc).AddTicks(3371));
        }
    }
}
