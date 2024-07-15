using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VilaId",
                table: "VillaNumber",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 14, 23, 22, 20, 882, DateTimeKind.Local).AddTicks(3579));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 14, 23, 22, 20, 882, DateTimeKind.Local).AddTicks(3646));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 14, 23, 22, 20, 882, DateTimeKind.Local).AddTicks(3651));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 14, 23, 22, 20, 882, DateTimeKind.Local).AddTicks(3655));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 14, 23, 22, 20, 882, DateTimeKind.Local).AddTicks(3659));

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumber_VilaId",
                table: "VillaNumber",
                column: "VilaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumber_Villas_VilaId",
                table: "VillaNumber",
                column: "VilaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumber_Villas_VilaId",
                table: "VillaNumber");

            migrationBuilder.DropIndex(
                name: "IX_VillaNumber_VilaId",
                table: "VillaNumber");

            migrationBuilder.DropColumn(
                name: "VilaId",
                table: "VillaNumber");

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
    }
}
