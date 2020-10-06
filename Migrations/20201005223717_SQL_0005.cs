using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_0005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "NotificationPerson");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "NotificationPerson");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Notification");

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 5, 19, 37, 16, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 5, 19, 37, 16, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 5, 19, 37, 16, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 5, 19, 37, 16, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 10, 5, 19, 37, 16, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 10, 5, 19, 37, 16, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 10, 5, 19, 37, 16, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 10, 5, 19, 37, 16, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "NotificationPerson",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "NotificationPerson",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Notification",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Notification",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 9, 29, 21, 44, 45, 0, DateTimeKind.Unspecified), new DateTime(2020, 9, 29, 21, 44, 45, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 9, 29, 21, 44, 45, 0, DateTimeKind.Unspecified), new DateTime(2020, 9, 29, 21, 44, 45, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 9, 29, 21, 44, 45, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 9, 29, 21, 44, 45, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 9, 29, 21, 44, 45, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 9, 29, 21, 44, 45, 0, DateTimeKind.Unspecified));
        }
    }
}
