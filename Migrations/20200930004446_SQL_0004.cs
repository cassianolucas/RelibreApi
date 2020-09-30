using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_0004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "library_book",
                newName: "updated_at");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "library_book",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

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
                columns: new[] { "created_at", "description" },
                values: new object[] { new DateTime(2020, 9, 29, 21, 44, 45, 0, DateTimeKind.Unspecified), "Trocar" });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "description" },
                values: new object[] { new DateTime(2020, 9, 29, 21, 44, 45, 0, DateTimeKind.Unspecified), "Doar" });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                columns: new[] { "created_at", "description" },
                values: new object[] { new DateTime(2020, 9, 29, 21, 44, 45, 0, DateTimeKind.Unspecified), "Emprestar" });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 9, 29, 21, 44, 45, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "library_book",
                newName: "UpdatedAt");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "library_book",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 9, 28, 22, 1, 16, 0, DateTimeKind.Unspecified), new DateTime(2020, 9, 28, 22, 1, 16, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 9, 28, 22, 1, 16, 0, DateTimeKind.Unspecified), new DateTime(2020, 9, 28, 22, 1, 16, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "description" },
                values: new object[] { new DateTime(2020, 9, 28, 22, 1, 16, 0, DateTimeKind.Unspecified), "Troca" });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "description" },
                values: new object[] { new DateTime(2020, 9, 28, 22, 1, 16, 0, DateTimeKind.Unspecified), "Doação" });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                columns: new[] { "created_at", "description" },
                values: new object[] { new DateTime(2020, 9, 28, 22, 1, 16, 0, DateTimeKind.Unspecified), "Emprestimo" });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 9, 28, 22, 1, 16, 0, DateTimeKind.Unspecified));
        }
    }
}
