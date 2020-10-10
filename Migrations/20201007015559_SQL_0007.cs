using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_0007 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationPerson_Notification_NotificationId",
                table: "NotificationPerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "notification");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "notification",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "notification",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "notification",
                newName: "created_at");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "notification",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "notification",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "notification",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_notification",
                table: "notification",
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

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationPerson_notification_NotificationId",
                table: "NotificationPerson",
                column: "NotificationId",
                principalTable: "notification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationPerson_notification_NotificationId",
                table: "NotificationPerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_notification",
                table: "notification");

            migrationBuilder.RenameTable(
                name: "notification",
                newName: "Notification");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Notification",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Notification",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Notification",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Notification",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Notification",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Notification",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationPerson_Notification_NotificationId",
                table: "NotificationPerson",
                column: "NotificationId",
                principalTable: "Notification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
