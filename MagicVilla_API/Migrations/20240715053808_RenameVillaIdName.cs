using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class RenameVillaIdName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumber_Villas_VilaId",
                table: "VillaNumber");

            migrationBuilder.RenameColumn(
                name: "VilaId",
                table: "VillaNumber",
                newName: "VillaId");

            migrationBuilder.RenameIndex(
                name: "IX_VillaNumber_VilaId",
                table: "VillaNumber",
                newName: "IX_VillaNumber_VillaId");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 15, 0, 38, 7, 815, DateTimeKind.Local).AddTicks(2466));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 15, 0, 38, 7, 815, DateTimeKind.Local).AddTicks(2522));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 15, 0, 38, 7, 815, DateTimeKind.Local).AddTicks(2526));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 15, 0, 38, 7, 815, DateTimeKind.Local).AddTicks(2528));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 15, 0, 38, 7, 815, DateTimeKind.Local).AddTicks(2530));

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumber_Villas_VillaId",
                table: "VillaNumber",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumber_Villas_VillaId",
                table: "VillaNumber");

            migrationBuilder.RenameColumn(
                name: "VillaId",
                table: "VillaNumber",
                newName: "VilaId");

            migrationBuilder.RenameIndex(
                name: "IX_VillaNumber_VillaId",
                table: "VillaNumber",
                newName: "IX_VillaNumber_VilaId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumber_Villas_VilaId",
                table: "VillaNumber",
                column: "VilaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
