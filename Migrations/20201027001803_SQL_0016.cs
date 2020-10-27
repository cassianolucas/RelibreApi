using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_0016 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "extra_large",
                table: "image");

            migrationBuilder.DropColumn(
                name: "large",
                table: "image");

            migrationBuilder.DropColumn(
                name: "medium",
                table: "image");

            migrationBuilder.DropColumn(
                name: "small",
                table: "image");

            migrationBuilder.DropColumn(
                name: "small_thumbnail",
                table: "image");

            migrationBuilder.DropColumn(
                name: "thumbnail",
                table: "image");

            migrationBuilder.RenameColumn(
                name: "birthdate",
                table: "person",
                newName: "birth_date");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "type",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "image_link",
                table: "image",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 26, 21, 18, 3, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 26, 21, 18, 3, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 26, 21, 18, 3, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 26, 21, 18, 3, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 10, 26, 21, 18, 3, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 10, 26, 21, 18, 3, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 10, 26, 21, 18, 3, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 10, 26, 21, 18, 3, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "type");

            migrationBuilder.DropColumn(
                name: "image_link",
                table: "image");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                table: "person",
                newName: "birthdate");

            migrationBuilder.AddColumn<string>(
                name: "extra_large",
                table: "image",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "large",
                table: "image",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "medium",
                table: "image",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "small",
                table: "image",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "small_thumbnail",
                table: "image",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "thumbnail",
                table: "image",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 22, 19, 56, 45, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 22, 19, 56, 45, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 10, 22, 19, 56, 45, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 22, 19, 56, 45, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 10, 22, 19, 56, 45, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 10, 22, 19, 56, 45, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 10, 22, 19, 56, 45, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 10, 22, 19, 56, 45, 0, DateTimeKind.Unspecified));
        }
    }
}
