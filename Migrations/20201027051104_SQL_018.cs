using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RatingValue",
                table: "user",
                newName: "TotalValue");

            migrationBuilder.RenameColumn(
                name: "rating_count",
                table: "user",
                newName: "total_count");

            migrationBuilder.RenameColumn(
                name: "available",
                table: "contact_book",
                newName: "approved");

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 27, 2, 11, 4, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 27, 2, 11, 4, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 27, 2, 11, 4, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 27, 2, 11, 4, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 10, 27, 2, 11, 4, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 10, 27, 2, 11, 4, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 10, 27, 2, 11, 4, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 10, 27, 2, 11, 4, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalValue",
                table: "user",
                newName: "RatingValue");

            migrationBuilder.RenameColumn(
                name: "total_count",
                table: "user",
                newName: "rating_count");

            migrationBuilder.RenameColumn(
                name: "approved",
                table: "contact_book",
                newName: "available");

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
    }
}
