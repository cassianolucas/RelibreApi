using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_0017 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "denied",
                table: "contact_book",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 27, 0, 41, 29, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 27, 0, 41, 29, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 27, 0, 41, 29, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 27, 0, 41, 29, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 10, 27, 0, 41, 29, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 10, 27, 0, 41, 29, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 10, 27, 0, 41, 29, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 10, 27, 0, 41, 29, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "denied",
                table: "contact_book");

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 26, 21, 18, 3, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 26, 21, 18, 3, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 26, 21, 18, 3, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 26, 21, 18, 3, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 10, 26, 21, 18, 3, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 10, 26, 21, 18, 3, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 10, 26, 21, 18, 3, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 10, 26, 21, 18, 3, 0, DateTimeKind.Unspecified));
        }
    }
}
