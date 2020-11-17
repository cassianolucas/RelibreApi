using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 16, 21, 54, 44, 9, DateTimeKind.Local).AddTicks(8080), new DateTime(2020, 11, 16, 21, 54, 44, 10, DateTimeKind.Local).AddTicks(4012) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 16, 21, 54, 44, 10, DateTimeKind.Local).AddTicks(4338), new DateTime(2020, 11, 16, 21, 54, 44, 10, DateTimeKind.Local).AddTicks(4349) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 21, 54, 44, 11, DateTimeKind.Local).AddTicks(2971));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 21, 54, 44, 11, DateTimeKind.Local).AddTicks(3005));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 21, 54, 44, 11, DateTimeKind.Local).AddTicks(3007));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 21, 54, 44, 11, DateTimeKind.Local).AddTicks(3009));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 5L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 21, 54, 44, 11, DateTimeKind.Local).AddTicks(3010));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 16, 20, 31, 55, 729, DateTimeKind.Local).AddTicks(9281), new DateTime(2020, 11, 16, 20, 31, 55, 730, DateTimeKind.Local).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 16, 20, 31, 55, 730, DateTimeKind.Local).AddTicks(6816), new DateTime(2020, 11, 16, 20, 31, 55, 730, DateTimeKind.Local).AddTicks(6826) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 20, 31, 55, 731, DateTimeKind.Local).AddTicks(6042));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 20, 31, 55, 731, DateTimeKind.Local).AddTicks(6081));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 20, 31, 55, 731, DateTimeKind.Local).AddTicks(6084));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 20, 31, 55, 731, DateTimeKind.Local).AddTicks(6085));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 5L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 20, 31, 55, 731, DateTimeKind.Local).AddTicks(6086));
        }
    }
}
