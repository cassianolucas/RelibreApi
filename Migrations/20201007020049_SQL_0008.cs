using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_0008 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationPerson_notification_NotificationId",
                table: "NotificationPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationPerson_person_PersonId",
                table: "NotificationPerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationPerson",
                table: "NotificationPerson");

            migrationBuilder.DropIndex(
                name: "IX_NotificationPerson_NotificationId",
                table: "NotificationPerson");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                table: "NotificationPerson");

            migrationBuilder.RenameTable(
                name: "NotificationPerson",
                newName: "notification_person");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "notification_person",
                newName: "active");

            migrationBuilder.RenameColumn(
                name: "IdPerson",
                table: "notification_person",
                newName: "id_person");

            migrationBuilder.RenameColumn(
                name: "IdNotification",
                table: "notification_person",
                newName: "id_notification");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "notification_person",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationPerson_PersonId",
                table: "notification_person",
                newName: "IX_notification_person_PersonId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "notification_person",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_notification_person",
                table: "notification_person",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ContactBook",
                columns: table => new
                {
                    id_contact = table.Column<long>(type: "bigint", nullable: false),
                    id_library_book = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactBook", x => new { x.id_contact, x.id_library_book });
                    table.ForeignKey(
                        name: "fk_contact_book_contact_id_contact",
                        column: x => x.id_contact,
                        principalTable: "contact",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_contact_book_library_book_id_library_book",
                        column: x => x.id_library_book,
                        principalTable: "library_book",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 6, 23, 0, 49, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 6, 23, 0, 49, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 6, 23, 0, 49, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 6, 23, 0, 49, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 23, 0, 49, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 23, 0, 49, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 23, 0, 49, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 23, 0, 49, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_notification_person_id_notification",
                table: "notification_person",
                column: "id_notification");

            migrationBuilder.CreateIndex(
                name: "IX_ContactBook_id_library_book",
                table: "ContactBook",
                column: "id_library_book");

            migrationBuilder.AddForeignKey(
                name: "fk_notification_person_notification_id_notification",
                table: "notification_person",
                column: "id_notification",
                principalTable: "notification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_notification_person_person_PersonId",
                table: "notification_person",
                column: "PersonId",
                principalTable: "person",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_notification_person_notification_id_notification",
                table: "notification_person");

            migrationBuilder.DropForeignKey(
                name: "FK_notification_person_person_PersonId",
                table: "notification_person");

            migrationBuilder.DropTable(
                name: "ContactBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_notification_person",
                table: "notification_person");

            migrationBuilder.DropIndex(
                name: "IX_notification_person_id_notification",
                table: "notification_person");

            migrationBuilder.RenameTable(
                name: "notification_person",
                newName: "NotificationPerson");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "NotificationPerson",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "id_person",
                table: "NotificationPerson",
                newName: "IdPerson");

            migrationBuilder.RenameColumn(
                name: "id_notification",
                table: "NotificationPerson",
                newName: "IdNotification");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "NotificationPerson",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_notification_person_PersonId",
                table: "NotificationPerson",
                newName: "IX_NotificationPerson_PersonId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "NotificationPerson",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AddColumn<long>(
                name: "NotificationId",
                table: "NotificationPerson",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationPerson",
                table: "NotificationPerson",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 6, 22, 55, 59, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 6, 22, 55, 59, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 6, 22, 55, 59, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 6, 22, 55, 59, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 22, 55, 59, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 22, 55, 59, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 22, 55, 59, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 10, 6, 22, 55, 59, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_NotificationPerson_NotificationId",
                table: "NotificationPerson",
                column: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationPerson_notification_NotificationId",
                table: "NotificationPerson",
                column: "NotificationId",
                principalTable: "notification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationPerson_person_PersonId",
                table: "NotificationPerson",
                column: "PersonId",
                principalTable: "person",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
