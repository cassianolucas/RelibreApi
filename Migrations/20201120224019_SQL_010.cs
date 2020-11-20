using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "avaliable",
                table: "contact_book",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 20, 19, 40, 19, 378, DateTimeKind.Local).AddTicks(9815), new DateTime(2020, 11, 20, 19, 40, 19, 379, DateTimeKind.Local).AddTicks(5741) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 20, 19, 40, 19, 379, DateTimeKind.Local).AddTicks(6064), new DateTime(2020, 11, 20, 19, 40, 19, 379, DateTimeKind.Local).AddTicks(6075) });

            migrationBuilder.UpdateData(
                table: "subscription",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 11, 20, 19, 40, 19, 380, DateTimeKind.Local).AddTicks(5785));

            migrationBuilder.UpdateData(
                table: "subscription",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 11, 20, 19, 40, 19, 380, DateTimeKind.Local).AddTicks(5830));

            migrationBuilder.UpdateData(
                table: "subscription",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 11, 20, 19, 40, 19, 380, DateTimeKind.Local).AddTicks(5833));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 11, 20, 19, 40, 19, 380, DateTimeKind.Local).AddTicks(4661));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 11, 20, 19, 40, 19, 380, DateTimeKind.Local).AddTicks(4696));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 11, 20, 19, 40, 19, 380, DateTimeKind.Local).AddTicks(4698));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 11, 20, 19, 40, 19, 380, DateTimeKind.Local).AddTicks(4700));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 5L,
                column: "created_at",
                value: new DateTime(2020, 11, 20, 19, 40, 19, 380, DateTimeKind.Local).AddTicks(4701));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avaliable",
                table: "contact_book");

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
    }
}
