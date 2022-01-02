using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace adnumaZ.Data.Migrations
{
    public partial class DownloadedFavouritedMappingTablesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FavouritedAt",
                table: "UserFavouritedTorrents",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 28, 7, 18, 18, 204, DateTimeKind.Utc).AddTicks(6045));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DownloadedAt",
                table: "UserDownloadedTorrents",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 28, 7, 18, 18, 210, DateTimeKind.Utc).AddTicks(4968));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 1, 2, 14, 21, 6, 306, DateTimeKind.Utc).AddTicks(5143),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 12, 28, 7, 18, 18, 214, DateTimeKind.Utc).AddTicks(436));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FavouritedAt",
                table: "UserFavouritedTorrents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 7, 18, 18, 204, DateTimeKind.Utc).AddTicks(6045),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DownloadedAt",
                table: "UserDownloadedTorrents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 7, 18, 18, 210, DateTimeKind.Utc).AddTicks(4968),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 12, 28, 7, 18, 18, 214, DateTimeKind.Utc).AddTicks(436),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 1, 2, 14, 21, 6, 306, DateTimeKind.Utc).AddTicks(5143));
        }
    }
}
