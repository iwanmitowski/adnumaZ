using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace adnumaZ.Data.Migrations
{
    public partial class DefaultCreatedOnValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 11, 15, 7, 47, 7, 161, DateTimeKind.Utc).AddTicks(3143),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 11, 15, 7, 47, 7, 161, DateTimeKind.Utc).AddTicks(3143));
        }
    }
}
