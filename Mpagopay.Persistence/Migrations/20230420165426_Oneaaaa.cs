using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mpagopay.Persistence.Migrations
{
    public partial class Oneaaaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 1L);

            migrationBuilder.AlterColumn<string>(
                name: "Expires",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Expires",
                table: "Cards",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardId", "CreateBy", "CreateDate", "Cvv", "Expires", "LastModifiedBy", "LastModifiedDate", "Name", "Number" },
                values: new object[] { 1L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KAMDJO TEGUIA HERVE", "" });
        }
    }
}
