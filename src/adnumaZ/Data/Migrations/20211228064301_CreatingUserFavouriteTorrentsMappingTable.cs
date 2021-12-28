using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace adnumaZ.Data.Migrations
{
    public partial class CreatingUserFavouriteTorrentsMappingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TorrentUser");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DownloadedAt",
                table: "UserDownloadedTorrents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 6, 43, 1, 369, DateTimeKind.Utc).AddTicks(1905),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 27, 11, 29, 2, 876, DateTimeKind.Utc).AddTicks(5681));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 12, 28, 6, 43, 1, 374, DateTimeKind.Utc).AddTicks(4966),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 12, 27, 11, 29, 2, 890, DateTimeKind.Utc).AddTicks(3513));

            migrationBuilder.CreateTable(
                name: "UserFavouritedTorrents",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TorrentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavouritedTorrents", x => new { x.TorrentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserFavouritedTorrents_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavouritedTorrents_Torrents_TorrentId",
                        column: x => x.TorrentId,
                        principalTable: "Torrents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavouritedTorrents_UserId",
                table: "UserFavouritedTorrents",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavouritedTorrents");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DownloadedAt",
                table: "UserDownloadedTorrents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 27, 11, 29, 2, 876, DateTimeKind.Utc).AddTicks(5681),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 28, 6, 43, 1, 369, DateTimeKind.Utc).AddTicks(1905));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 12, 27, 11, 29, 2, 890, DateTimeKind.Utc).AddTicks(3513),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 12, 28, 6, 43, 1, 374, DateTimeKind.Utc).AddTicks(4966));

            migrationBuilder.CreateTable(
                name: "TorrentUser",
                columns: table => new
                {
                    FavouriteTorrentsId = table.Column<int>(type: "int", nullable: false),
                    FavouritedByUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TorrentUser", x => new { x.FavouriteTorrentsId, x.FavouritedByUsersId });
                    table.ForeignKey(
                        name: "FK_TorrentUser_AspNetUsers_FavouritedByUsersId",
                        column: x => x.FavouritedByUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TorrentUser_Torrents_FavouriteTorrentsId",
                        column: x => x.FavouriteTorrentsId,
                        principalTable: "Torrents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TorrentUser_FavouritedByUsersId",
                table: "TorrentUser",
                column: "FavouritedByUsersId");
        }
    }
}
