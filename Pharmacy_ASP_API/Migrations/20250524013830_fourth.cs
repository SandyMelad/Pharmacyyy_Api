using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy_ASP_API.Migrations
{
    /// <inheritdoc />
    public partial class fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_MedicationKnowledges_MedicationId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_MedicationId",
                table: "Stocks");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId1",
                table: "MedicationRequests",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_MedicationId",
                table: "Stocks",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationRequests_OrderId1",
                table: "MedicationRequests",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationRequests_Orders_OrderId1",
                table: "MedicationRequests",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_MedicationKnowledges_MedicationId",
                table: "Stocks",
                column: "MedicationId",
                principalTable: "MedicationKnowledges",
                principalColumn: "MedicationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationRequests_Orders_OrderId1",
                table: "MedicationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_MedicationKnowledges_MedicationId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_MedicationId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_MedicationRequests_OrderId1",
                table: "MedicationRequests");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "MedicationRequests");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_MedicationId",
                table: "Stocks",
                column: "MedicationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_MedicationKnowledges_MedicationId",
                table: "Stocks",
                column: "MedicationId",
                principalTable: "MedicationKnowledges",
                principalColumn: "MedicationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
