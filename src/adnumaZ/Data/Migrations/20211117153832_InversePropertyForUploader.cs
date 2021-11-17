using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace adnumaZ.Data.Migrations
{
    public partial class InversePropertyForUploader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 11, 17, 15, 38, 31, 818, DateTimeKind.Utc).AddTicks(8266),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 11, 15, 7, 47, 7, 161, DateTimeKind.Utc).AddTicks(3143));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 11, 15, 7, 47, 7, 161, DateTimeKind.Utc).AddTicks(3143),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 11, 17, 15, 38, 31, 818, DateTimeKind.Utc).AddTicks(8266));
        }
    }
}
