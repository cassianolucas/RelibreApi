using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RelibreApi.Migrations
{
    public partial class SQL_007 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "validate",
                table: "person_subscription",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 16, 23, 24, 29, 466, DateTimeKind.Local).AddTicks(1618), new DateTime(2020, 11, 16, 23, 24, 29, 466, DateTimeKind.Local).AddTicks(7550) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 16, 23, 24, 29, 466, DateTimeKind.Local).AddTicks(7942), new DateTime(2020, 11, 16, 23, 24, 29, 466, DateTimeKind.Local).AddTicks(7952) });

            migrationBuilder.InsertData(
                table: "subscription",
                columns: new[] { "id", "created_at", "description", "period", "value" },
                values: new object[,]
                {
                    { 1L, new DateTime(2020, 11, 16, 23, 24, 29, 467, DateTimeKind.Local).AddTicks(8119), "Pacote de 1 mês", 1, 15m },
                    { 2L, new DateTime(2020, 11, 16, 23, 24, 29, 467, DateTimeKind.Local).AddTicks(8185), "Pacote de 3 meses", 3, 39m },
                    { 3L, new DateTime(2020, 11, 16, 23, 24, 29, 467, DateTimeKind.Local).AddTicks(8187), "Pacote de 6 meses", 6, 60m }
                });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 24, 29, 467, DateTimeKind.Local).AddTicks(6839));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 24, 29, 467, DateTimeKind.Local).AddTicks(6875));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 24, 29, 467, DateTimeKind.Local).AddTicks(6878));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 24, 29, 467, DateTimeKind.Local).AddTicks(6879));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 5L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 23, 24, 29, 467, DateTimeKind.Local).AddTicks(6880));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscription",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "subscription",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "subscription",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.AlterColumn<string>(
                name: "validate",
                table: "person_subscription",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 16, 21, 54, 44, 9, DateTimeKind.Local).AddTicks(8080), new DateTime(2020, 11, 16, 21, 54, 44, 10, DateTimeKind.Local).AddTicks(4012) });

            migrationBuilder.UpdateData(
                table: "profile",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2020, 11, 16, 21, 54, 44, 10, DateTimeKind.Local).AddTicks(4338), new DateTime(2020, 11, 16, 21, 54, 44, 10, DateTimeKind.Local).AddTicks(4349) });

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 21, 54, 44, 11, DateTimeKind.Local).AddTicks(2971));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 21, 54, 44, 11, DateTimeKind.Local).AddTicks(3005));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 21, 54, 44, 11, DateTimeKind.Local).AddTicks(3007));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 4L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 21, 54, 44, 11, DateTimeKind.Local).AddTicks(3009));

            migrationBuilder.UpdateData(
                table: "type",
                keyColumn: "id",
                keyValue: 5L,
                column: "created_at",
                value: new DateTime(2020, 11, 16, 21, 54, 44, 11, DateTimeKind.Local).AddTicks(3010));
        }
    }
}
