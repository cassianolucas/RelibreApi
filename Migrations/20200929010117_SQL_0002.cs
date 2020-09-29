using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_0002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_address_person_PersonId",
                table: "address");

            migrationBuilder.DropIndex(
                name: "IX_address_PersonId",
                table: "address");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "address");

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
                column: "created_at",
                value: new DateTime(2020, 9, 28, 22, 1, 16, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 9, 28, 22, 1, 16, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 9, 28, 22, 1, 16, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 9, 28, 22, 1, 16, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_address_id_person",
                table: "address",
                column: "id_person");

            migrationBuilder.AddForeignKey(
                name: "fk_adress_person_id_person",
                table: "address",
                column: "id_person",
                principalTable: "person",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_adress_person_id_person",
                table: "address");

            migrationBuilder.DropIndex(
                name: "IX_address_id_person",
                table: "address");

            migrationBuilder.AddColumn<long>(
                name: "PersonId",
                table: "address",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 9, 28, 21, 51, 18, 0, DateTimeKind.Unspecified), new DateTime(2020, 9, 28, 21, 51, 18, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 9, 28, 21, 51, 18, 0, DateTimeKind.Unspecified), new DateTime(2020, 9, 28, 21, 51, 18, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 9, 28, 21, 51, 18, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 9, 28, 21, 51, 18, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 9, 28, 21, 51, 18, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 9, 28, 21, 51, 18, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_address_PersonId",
                table: "address",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_address_person_PersonId",
                table: "address",
                column: "PersonId",
                principalTable: "person",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
