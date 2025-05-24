using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy_ASP_API.Migrations
{
    /// <inheritdoc />
    public partial class foriegnkey : Migration
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

            migrationBuilder.DropColumn(
                name: "MedicationId",
                table: "Stocks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MedicationId",
                table: "Stocks",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_MedicationId",
                table: "Stocks",
                column: "MedicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_MedicationKnowledges_MedicationId",
                table: "Stocks",
                column: "MedicationId",
                principalTable: "MedicationKnowledges",
                principalColumn: "MedicationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
