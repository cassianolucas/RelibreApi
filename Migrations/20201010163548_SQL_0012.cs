using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_0012 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contact_book_contact_id_contact",
                table: "contact_book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contact_book",
                table: "contact_book");

            migrationBuilder.RenameColumn(
                name: "id_contact",
                table: "contact_book",
                newName: "id_contact_request");

            migrationBuilder.AddColumn<long>(
                name: "id_contact_owner",
                table: "contact_book",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "contact",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contact_book",
                table: "contact_book",
                columns: new[] { "id_contact_owner", "id_contact_request", "id_library_book" });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 10, 13, 35, 48, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 10, 13, 35, 48, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 10, 13, 35, 48, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 10, 13, 35, 48, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 10, 10, 13, 35, 48, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 10, 10, 13, 35, 48, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 10, 10, 13, 35, 48, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 10, 10, 13, 35, 48, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_contact_book_id_contact_request",
                table: "contact_book",
                column: "id_contact_request");

            migrationBuilder.CreateIndex(
                name: "IX_contact_email",
                table: "contact",
                column: "email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_contact_book_owner_contact_id_contact",
                table: "contact_book",
                column: "id_contact_owner",
                principalTable: "contact",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_contact_book_request_contact_id_contact",
                table: "contact_book",
                column: "id_contact_request",
                principalTable: "contact",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contact_book_owner_contact_id_contact",
                table: "contact_book");

            migrationBuilder.DropForeignKey(
                name: "fk_contact_book_request_contact_id_contact",
                table: "contact_book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contact_book",
                table: "contact_book");

            migrationBuilder.DropIndex(
                name: "IX_contact_book_id_contact_request",
                table: "contact_book");

            migrationBuilder.DropIndex(
                name: "IX_contact_email",
                table: "contact");

            migrationBuilder.DropColumn(
                name: "id_contact_owner",
                table: "contact_book");

            migrationBuilder.DropColumn(
                name: "name",
                table: "contact");

            migrationBuilder.RenameColumn(
                name: "id_contact_request",
                table: "contact_book",
                newName: "id_contact");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contact_book",
                table: "contact_book",
                columns: new[] { "id_contact", "id_library_book" });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 7, 19, 40, 17, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 7, 19, 40, 17, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 7, 19, 40, 17, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 7, 19, 40, 17, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 10, 7, 19, 40, 17, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 10, 7, 19, 40, 17, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 10, 7, 19, 40, 17, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 10, 7, 19, 40, 17, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "fk_contact_book_contact_id_contact",
                table: "contact_book",
                column: "id_contact",
                principalTable: "contact",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
