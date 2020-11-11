using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "person",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "url_image",
                table: "person",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "web_site",
                table: "person",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 10, 19, 59, 25, 935, DateTimeKind.Local).AddTicks(177), new DateTime(2020, 11, 10, 19, 59, 25, 935, DateTimeKind.Local).AddTicks(6405) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 10, 19, 59, 25, 935, DateTimeKind.Local).AddTicks(6754), new DateTime(2020, 11, 10, 19, 59, 25, 935, DateTimeKind.Local).AddTicks(6763) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 11, 10, 19, 59, 25, 936, DateTimeKind.Local).AddTicks(5728));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 11, 10, 19, 59, 25, 936, DateTimeKind.Local).AddTicks(5769));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 11, 10, 19, 59, 25, 936, DateTimeKind.Local).AddTicks(5771));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 11, 10, 19, 59, 25, 936, DateTimeKind.Local).AddTicks(5772));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "person");

            migrationBuilder.DropColumn(
                name: "url_image",
                table: "person");

            migrationBuilder.DropColumn(
                name: "web_site",
                table: "person");

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 5, 22, 4, 19, 0, DateTimeKind.Unspecified), new DateTime(2020, 11, 5, 22, 4, 19, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 5, 22, 4, 19, 0, DateTimeKind.Unspecified), new DateTime(2020, 11, 5, 22, 4, 19, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 11, 5, 22, 4, 19, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 11, 5, 22, 4, 19, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 11, 5, 22, 4, 19, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 11, 5, 22, 4, 19, 0, DateTimeKind.Unspecified));
        }
    }
}
