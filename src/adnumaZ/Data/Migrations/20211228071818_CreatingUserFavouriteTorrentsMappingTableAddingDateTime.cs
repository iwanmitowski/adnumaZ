using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace adnumaZ.Data.Migrations
{
    public partial class CreatingUserFavouriteTorrentsMappingTableAddingDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FavouritedAt",
                table: "UserFavouritedTorrents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 7, 18, 18, 204, DateTimeKind.Utc).AddTicks(6045));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DownloadedAt",
                table: "UserDownloadedTorrents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 7, 18, 18, 210, DateTimeKind.Utc).AddTicks(4968),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 28, 6, 43, 1, 369, DateTimeKind.Utc).AddTicks(1905));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 12, 28, 7, 18, 18, 214, DateTimeKind.Utc).AddTicks(436),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 12, 28, 6, 43, 1, 374, DateTimeKind.Utc).AddTicks(4966));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavouritedAt",
                table: "UserFavouritedTorrents");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DownloadedAt",
                table: "UserDownloadedTorrents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 6, 43, 1, 369, DateTimeKind.Utc).AddTicks(1905),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 28, 7, 18, 18, 210, DateTimeKind.Utc).AddTicks(4968));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 12, 28, 6, 43, 1, 374, DateTimeKind.Utc).AddTicks(4966),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 12, 28, 7, 18, 18, 214, DateTimeKind.Utc).AddTicks(436));
        }
    }
}
