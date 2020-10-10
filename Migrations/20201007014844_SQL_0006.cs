using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_0006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_library_book_contact_id_contact",
                table: "library_book");

            migrationBuilder.DropIndex(
                name: "IX_library_book_id_contact",
                table: "library_book");

            migrationBuilder.DropColumn(
                name: "id_contact",
                table: "library_book");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "NotificationPerson",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 6, 22, 48, 44, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 6, 22, 48, 44, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 6, 22, 48, 44, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 6, 22, 48, 44, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 22, 48, 44, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 22, 48, 44, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 22, 48, 44, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 22, 48, 44, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "NotificationPerson");

            migrationBuilder.AddColumn<long>(
                name: "id_contact",
                table: "library_book",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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

            migrationBuilder.CreateIndex(
                name: "IX_library_book_id_contact",
                table: "library_book",
                column: "id_contact");

            migrationBuilder.AddForeignKey(
                name: "fk_library_book_contact_id_contact",
                table: "library_book",
                column: "id_contact",
                principalTable: "contact",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
