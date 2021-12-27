using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace adnumaZ.Data.Migrations
{
    public partial class CreatingUserDownloadMappingTableFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TorrentUser_AspNetUsers_DownloadersId",
                table: "TorrentUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TorrentUser_Torrents_DownloadedTorrentsId",
                table: "TorrentUser");

            migrationBuilder.DropTable(
                name: "TorrentUser1");

            migrationBuilder.RenameColumn(
                name: "DownloadersId",
                table: "TorrentUser",
                newName: "FavouritedByUsersId");

            migrationBuilder.RenameColumn(
                name: "DownloadedTorrentsId",
                table: "TorrentUser",
                newName: "FavouriteTorrentsId");

            migrationBuilder.RenameIndex(
                name: "IX_TorrentUser_DownloadersId",
                table: "TorrentUser",
                newName: "IX_TorrentUser_FavouritedByUsersId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 12, 27, 11, 29, 2, 890, DateTimeKind.Utc).AddTicks(3513),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 11, 28, 20, 45, 17, 333, DateTimeKind.Utc).AddTicks(3371));

            migrationBuilder.CreateTable(
                name: "UserDownloadedTorrents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TorrentId = table.Column<int>(type: "int", nullable: false),
                    DownloadedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 12, 27, 11, 29, 2, 876, DateTimeKind.Utc).AddTicks(5681))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDownloadedTorrents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDownloadedTorrents_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDownloadedTorrents_Torrents_TorrentId",
                        column: x => x.TorrentId,
                        principalTable: "Torrents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDownloadedTorrents_TorrentId",
                table: "UserDownloadedTorrents",
                column: "TorrentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDownloadedTorrents_UserId",
                table: "UserDownloadedTorrents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TorrentUser_AspNetUsers_FavouritedByUsersId",
                table: "TorrentUser",
                column: "FavouritedByUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TorrentUser_Torrents_FavouriteTorrentsId",
                table: "TorrentUser",
                column: "FavouriteTorrentsId",
                principalTable: "Torrents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TorrentUser_AspNetUsers_FavouritedByUsersId",
                table: "TorrentUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TorrentUser_Torrents_FavouriteTorrentsId",
                table: "TorrentUser");

            migrationBuilder.DropTable(
                name: "UserDownloadedTorrents");

            migrationBuilder.RenameColumn(
                name: "FavouritedByUsersId",
                table: "TorrentUser",
                newName: "DownloadersId");

            migrationBuilder.RenameColumn(
                name: "FavouriteTorrentsId",
                table: "TorrentUser",
                newName: "DownloadedTorrentsId");

            migrationBuilder.RenameIndex(
                name: "IX_TorrentUser_FavouritedByUsersId",
                table: "TorrentUser",
                newName: "IX_TorrentUser_DownloadersId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 11, 28, 20, 45, 17, 333, DateTimeKind.Utc).AddTicks(3371),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 12, 27, 11, 29, 2, 890, DateTimeKind.Utc).AddTicks(3513));

            migrationBuilder.CreateTable(
                name: "TorrentUser1",
                columns: table => new
                {
                    FavouriteTorrentsId = table.Column<int>(type: "int", nullable: false),
                    FavouritedByUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TorrentUser1", x => new { x.FavouriteTorrentsId, x.FavouritedByUsersId });
                    table.ForeignKey(
                        name: "FK_TorrentUser1_AspNetUsers_FavouritedByUsersId",
                        column: x => x.FavouritedByUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TorrentUser1_Torrents_FavouriteTorrentsId",
                        column: x => x.FavouriteTorrentsId,
                        principalTable: "Torrents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TorrentUser1_FavouritedByUsersId",
                table: "TorrentUser1",
                column: "FavouritedByUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_TorrentUser_AspNetUsers_DownloadersId",
                table: "TorrentUser",
                column: "DownloadersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TorrentUser_Torrents_DownloadedTorrentsId",
                table: "TorrentUser",
                column: "DownloadedTorrentsId",
                principalTable: "Torrents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
