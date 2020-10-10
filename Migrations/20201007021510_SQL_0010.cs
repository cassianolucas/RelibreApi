using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_0010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactBook",
                table: "ContactBook");

            migrationBuilder.RenameTable(
                name: "ContactBook",
                newName: "contact_book");

            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "person",
                newName: "birthday");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "contact",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "IdCategory",
                table: "category_book",
                newName: "id_category");

            migrationBuilder.RenameColumn(
                name: "IdBook",
                table: "category_book",
                newName: "id_book");

            migrationBuilder.RenameIndex(
                name: "IX_category_book_IdCategory",
                table: "category_book",
                newName: "IX_category_book_id_category");

            migrationBuilder.RenameColumn(
                name: "IdAuthor",
                table: "author_book",
                newName: "id_author");

            migrationBuilder.RenameColumn(
                name: "IdBook",
                table: "author_book",
                newName: "id_book");

            migrationBuilder.RenameIndex(
                name: "IX_author_book_IdAuthor",
                table: "author_book",
                newName: "IX_author_book_id_author");

            migrationBuilder.RenameIndex(
                name: "IX_ContactBook_id_library_book",
                table: "contact_book",
                newName: "IX_contact_book_id_library_book");

            migrationBuilder.AlterColumn<DateTime>(
                name: "birthday",
                table: "person",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contact_book",
                table: "contact_book",
                columns: new[] { "id_contact", "id_library_book" });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 6, 23, 15, 10, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 6, 23, 15, 10, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 6, 23, 15, 10, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 6, 23, 15, 10, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 23, 15, 10, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 23, 15, 10, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 23, 15, 10, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 23, 15, 10, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_contact_book",
                table: "contact_book");

            migrationBuilder.RenameTable(
                name: "contact_book",
                newName: "ContactBook");

            migrationBuilder.RenameColumn(
                name: "birthday",
                table: "person",
                newName: "Birthday");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "contact",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id_category",
                table: "category_book",
                newName: "IdCategory");

            migrationBuilder.RenameColumn(
                name: "id_book",
                table: "category_book",
                newName: "IdBook");

            migrationBuilder.RenameIndex(
                name: "IX_category_book_id_category",
                table: "category_book",
                newName: "IX_category_book_IdCategory");

            migrationBuilder.RenameColumn(
                name: "id_author",
                table: "author_book",
                newName: "IdAuthor");

            migrationBuilder.RenameColumn(
                name: "id_book",
                table: "author_book",
                newName: "IdBook");

            migrationBuilder.RenameIndex(
                name: "IX_author_book_id_author",
                table: "author_book",
                newName: "IX_author_book_IdAuthor");

            migrationBuilder.RenameIndex(
                name: "IX_contact_book_id_library_book",
                table: "ContactBook",
                newName: "IX_ContactBook_id_library_book");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "person",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactBook",
                table: "ContactBook",
                columns: new[] { "id_contact", "id_library_book" });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 6, 23, 6, 16, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 6, 23, 6, 16, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 6, 23, 6, 16, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 6, 23, 6, 16, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 23, 6, 16, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 23, 6, 16, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 23, 6, 16, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 23, 6, 16, 0, DateTimeKind.Unspecified));
        }
    }
}
