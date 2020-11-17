using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_008 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "url_image",
                table: "person",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "url_image",
                table: "person",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 16, 23, 24, 29, 466, DateTimeKind.Local).AddTicks(1618), new DateTime(2020, 11, 16, 23, 24, 29, 466, DateTimeKind.Local).AddTicks(7550) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 16, 23, 24, 29, 466, DateTimeKind.Local).AddTicks(7942), new DateTime(2020, 11, 16, 23, 24, 29, 466, DateTimeKind.Local).AddTicks(7952) });

            migrationBuilder.UpdateData(
                table: "subscription",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 24, 29, 467, DateTimeKind.Local).AddTicks(8119));

            migrationBuilder.UpdateData(
                table: "subscription",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 24, 29, 467, DateTimeKind.Local).AddTicks(8185));

            migrationBuilder.UpdateData(
                table: "subscription",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 24, 29, 467, DateTimeKind.Local).AddTicks(8187));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 24, 29, 467, DateTimeKind.Local).AddTicks(6839));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 24, 29, 467, DateTimeKind.Local).AddTicks(6875));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 24, 29, 467, DateTimeKind.Local).AddTicks(6878));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 24, 29, 467, DateTimeKind.Local).AddTicks(6879));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 5L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 24, 29, 467, DateTimeKind.Local).AddTicks(6880));
        }
    }
}
