using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "address",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "complement",
                table: "address",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "neighborhood",
                table: "address",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "number",
                table: "address",
                type: "varchar(144)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "address",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "street",
                table: "address",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "zip_code",
                table: "address",
                type: "varchar(8)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "city",
                table: "address");

            migrationBuilder.DropColumn(
                name: "complement",
                table: "address");

            migrationBuilder.DropColumn(
                name: "neighborhood",
                table: "address");

            migrationBuilder.DropColumn(
                name: "number",
                table: "address");

            migrationBuilder.DropColumn(
                name: "state",
                table: "address");

            migrationBuilder.DropColumn(
                name: "street",
                table: "address");

            migrationBuilder.DropColumn(
                name: "zip_code",
                table: "address");

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 15, 18, 3, 3, 794, DateTimeKind.Local).AddTicks(7562), new DateTime(2020, 11, 15, 18, 3, 3, 795, DateTimeKind.Local).AddTicks(3336) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 15, 18, 3, 3, 795, DateTimeKind.Local).AddTicks(3666), new DateTime(2020, 11, 15, 18, 3, 3, 795, DateTimeKind.Local).AddTicks(3677) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 11, 15, 18, 3, 3, 796, DateTimeKind.Local).AddTicks(2210));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 11, 15, 18, 3, 3, 796, DateTimeKind.Local).AddTicks(2245));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 11, 15, 18, 3, 3, 796, DateTimeKind.Local).AddTicks(2247));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 11, 15, 18, 3, 3, 796, DateTimeKind.Local).AddTicks(2249));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 5L,
                column: "created_at",
                value: new DateTime(2020, 11, 15, 18, 3, 3, 796, DateTimeKind.Local).AddTicks(2250));
        }
    }
}
