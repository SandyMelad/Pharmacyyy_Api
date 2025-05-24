using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy_ASP_API.Migrations
{
    /// <inheritdoc />
    public partial class medicationrquestorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationRequests_Orders_OrderId",
                table: "MedicationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationRequests_Orders_OrderId1",
                table: "MedicationRequests");

            migrationBuilder.DropIndex(
                name: "IX_MedicationRequests_OrderId",
                table: "MedicationRequests");

            migrationBuilder.DropIndex(
                name: "IX_MedicationRequests_OrderId1",
                table: "MedicationRequests");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "MedicationRequests");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "MedicationRequests");

            migrationBuilder.AlterColumn<Guid>(
                name: "MedicationRequestId",
                table: "Orders",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MedicationRequestId",
                table: "Orders",
                column: "MedicationRequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_MedicationRequests_MedicationRequestId",
                table: "Orders",
                column: "MedicationRequestId",
                principalTable: "MedicationRequests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_MedicationRequests_MedicationRequestId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MedicationRequestId",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "MedicationRequestId",
                table: "Orders",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "MedicationRequests",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId1",
                table: "MedicationRequests",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationRequests_OrderId",
                table: "MedicationRequests",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicationRequests_OrderId1",
                table: "MedicationRequests",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationRequests_Orders_OrderId",
                table: "MedicationRequests",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationRequests_Orders_OrderId1",
                table: "MedicationRequests",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }
    }
}
