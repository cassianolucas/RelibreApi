using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_0009 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notification_person_person_PersonId",
                table: "notification_person");

            migrationBuilder.DropIndex(
                name: "IX_notification_person_PersonId",
                table: "notification_person");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "notification_person");

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

            migrationBuilder.CreateIndex(
                name: "IX_notification_person_id_person",
                table: "notification_person",
                column: "id_person");

            migrationBuilder.AddForeignKey(
                name: "fk_notification_person_person_id_person",
                table: "notification_person",
                column: "id_person",
                principalTable: "person",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_notification_person_person_id_person",
                table: "notification_person");

            migrationBuilder.DropIndex(
                name: "IX_notification_person_id_person",
                table: "notification_person");

            migrationBuilder.AddColumn<long>(
                name: "PersonId",
                table: "notification_person",
                type: "bigint",
                nullable: true);

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
                name: "IX_notification_person_PersonId",
                table: "notification_person",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_notification_person_person_PersonId",
                table: "notification_person",
                column: "PersonId",
                principalTable: "person",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
