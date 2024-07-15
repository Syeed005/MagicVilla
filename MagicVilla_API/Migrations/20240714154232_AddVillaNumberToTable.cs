using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AddVillaNumberToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VillaNumber",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillaNumber", x => x.VillaNo);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 14, 10, 42, 32, 325, DateTimeKind.Local).AddTicks(8860));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 14, 10, 42, 32, 325, DateTimeKind.Local).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 14, 10, 42, 32, 325, DateTimeKind.Local).AddTicks(8914));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 14, 10, 42, 32, 325, DateTimeKind.Local).AddTicks(8916));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 14, 10, 42, 32, 325, DateTimeKind.Local).AddTicks(8950));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VillaNumber");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 12, 22, 55, 30, 906, DateTimeKind.Local).AddTicks(2118));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 12, 22, 55, 30, 906, DateTimeKind.Local).AddTicks(2168));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 12, 22, 55, 30, 906, DateTimeKind.Local).AddTicks(2170));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 12, 22, 55, 30, 906, DateTimeKind.Local).AddTicks(2172));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 12, 22, 55, 30, 906, DateTimeKind.Local).AddTicks(2174));
        }
    }
}
