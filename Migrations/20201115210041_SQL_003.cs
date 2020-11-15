using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_library_book_image_id_library_book",
                table: "image");

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 15, 18, 0, 41, 299, DateTimeKind.Local).AddTicks(7451), new DateTime(2020, 11, 15, 18, 0, 41, 300, DateTimeKind.Local).AddTicks(3340) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 15, 18, 0, 41, 300, DateTimeKind.Local).AddTicks(3671), new DateTime(2020, 11, 15, 18, 0, 41, 300, DateTimeKind.Local).AddTicks(3681) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 11, 15, 18, 0, 41, 301, DateTimeKind.Local).AddTicks(2281));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 11, 15, 18, 0, 41, 301, DateTimeKind.Local).AddTicks(2323));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 11, 15, 18, 0, 41, 301, DateTimeKind.Local).AddTicks(2325));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 11, 15, 18, 0, 41, 301, DateTimeKind.Local).AddTicks(2327));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 5L,
                column: "created_at",
                value: new DateTime(2020, 11, 15, 18, 0, 41, 301, DateTimeKind.Local).AddTicks(2328));

            migrationBuilder.AddForeignKey(
                name: "fk_library_book_image_id_library_book",
                table: "image",
                column: "id_library_book",
                principalTable: "library_book",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_library_book_image_id_library_book",
                table: "image");

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

            migrationBuilder.AddForeignKey(
                name: "fk_library_book_image_id_library_book",
                table: "image",
                column: "id_library_book",
                principalTable: "library_book",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
