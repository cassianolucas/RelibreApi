using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_009 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "contact_book",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 18, 21, 6, 23, 204, DateTimeKind.Local).AddTicks(7980), new DateTime(2020, 11, 18, 21, 6, 23, 205, DateTimeKind.Local).AddTicks(3887) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 18, 21, 6, 23, 205, DateTimeKind.Local).AddTicks(4219), new DateTime(2020, 11, 18, 21, 6, 23, 205, DateTimeKind.Local).AddTicks(4228) });

            migrationBuilder.UpdateData(
                table: "subscription",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 11, 18, 21, 6, 23, 206, DateTimeKind.Local).AddTicks(4108));

            migrationBuilder.UpdateData(
                table: "subscription",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 11, 18, 21, 6, 23, 206, DateTimeKind.Local).AddTicks(4152));

            migrationBuilder.UpdateData(
                table: "subscription",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 11, 18, 21, 6, 23, 206, DateTimeKind.Local).AddTicks(4154));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 11, 18, 21, 6, 23, 206, DateTimeKind.Local).AddTicks(2926));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 11, 18, 21, 6, 23, 206, DateTimeKind.Local).AddTicks(2962));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 11, 18, 21, 6, 23, 206, DateTimeKind.Local).AddTicks(2964));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 11, 18, 21, 6, 23, 206, DateTimeKind.Local).AddTicks(2965));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 5L,
                column: "created_at",
                value: new DateTime(2020, 11, 18, 21, 6, 23, 206, DateTimeKind.Local).AddTicks(2966));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "contact_book");

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 16, 23, 33, 54, 140, DateTimeKind.Local).AddTicks(3615), new DateTime(2020, 11, 16, 23, 33, 54, 140, DateTimeKind.Local).AddTicks(9611) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 16, 23, 33, 54, 140, DateTimeKind.Local).AddTicks(9955), new DateTime(2020, 11, 16, 23, 33, 54, 140, DateTimeKind.Local).AddTicks(9964) });

            migrationBuilder.UpdateData(
                table: "subscription",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 33, 54, 142, DateTimeKind.Local).AddTicks(79));

            migrationBuilder.UpdateData(
                table: "subscription",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 33, 54, 142, DateTimeKind.Local).AddTicks(126));

            migrationBuilder.UpdateData(
                table: "subscription",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 33, 54, 142, DateTimeKind.Local).AddTicks(128));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 33, 54, 141, DateTimeKind.Local).AddTicks(8855));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 33, 54, 141, DateTimeKind.Local).AddTicks(8891));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 33, 54, 141, DateTimeKind.Local).AddTicks(8893));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 33, 54, 141, DateTimeKind.Local).AddTicks(8895));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 5L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 33, 54, 141, DateTimeKind.Local).AddTicks(8896));
        }
    }
}
