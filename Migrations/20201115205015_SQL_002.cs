using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "book");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "library_book",
                newName: "id");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "library_book",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 15, 17, 50, 15, 533, DateTimeKind.Local).AddTicks(9710), new DateTime(2020, 11, 15, 17, 50, 15, 534, DateTimeKind.Local).AddTicks(5743) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 15, 17, 50, 15, 534, DateTimeKind.Local).AddTicks(6090), new DateTime(2020, 11, 15, 17, 50, 15, 534, DateTimeKind.Local).AddTicks(6101) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 11, 15, 17, 50, 15, 535, DateTimeKind.Local).AddTicks(4903));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 11, 15, 17, 50, 15, 535, DateTimeKind.Local).AddTicks(4940));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 11, 15, 17, 50, 15, 535, DateTimeKind.Local).AddTicks(4942));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 11, 15, 17, 50, 15, 535, DateTimeKind.Local).AddTicks(4944));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 5L,
                column: "created_at",
                value: new DateTime(2020, 11, 15, 17, 50, 15, 535, DateTimeKind.Local).AddTicks(4945));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "library_book");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "library_book",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "book",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 11, 0, 25, 14, 699, DateTimeKind.Local).AddTicks(9842), new DateTime(2020, 11, 11, 0, 25, 14, 700, DateTimeKind.Local).AddTicks(5842) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 11, 0, 25, 14, 700, DateTimeKind.Local).AddTicks(6175), new DateTime(2020, 11, 11, 0, 25, 14, 700, DateTimeKind.Local).AddTicks(6185) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 11, 11, 0, 25, 14, 701, DateTimeKind.Local).AddTicks(4785));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 11, 11, 0, 25, 14, 701, DateTimeKind.Local).AddTicks(4822));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 11, 11, 0, 25, 14, 701, DateTimeKind.Local).AddTicks(4824));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 11, 11, 0, 25, 14, 701, DateTimeKind.Local).AddTicks(4825));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 5L,
                column: "created_at",
                value: new DateTime(2020, 11, 11, 0, 25, 14, 701, DateTimeKind.Local).AddTicks(4826));
        }
    }
}
