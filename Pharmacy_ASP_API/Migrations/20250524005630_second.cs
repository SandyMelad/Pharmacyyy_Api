using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy_ASP_API.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicationKnowledgeOrders");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "MedicationKnowledges",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationKnowledges_OrderId",
                table: "MedicationKnowledges",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationKnowledges_Orders_OrderId",
                table: "MedicationKnowledges",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationKnowledges_Orders_OrderId",
                table: "MedicationKnowledges");

            migrationBuilder.DropIndex(
                name: "IX_MedicationKnowledges_OrderId",
                table: "MedicationKnowledges");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "MedicationKnowledges");

            migrationBuilder.CreateTable(
                name: "MedicationKnowledgeOrders",
                columns: table => new
                {
                    MedicationKnowledgesMedicationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    OrdersOrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationKnowledgeOrders", x => new { x.MedicationKnowledgesMedicationId, x.OrdersOrderId });
                    table.ForeignKey(
                        name: "FK_MedicationKnowledgeOrders_MedicationKnowledges_MedicationKno~",
                        column: x => x.MedicationKnowledgesMedicationId,
                        principalTable: "MedicationKnowledges",
                        principalColumn: "MedicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationKnowledgeOrders_Orders_OrdersOrderId",
                        column: x => x.OrdersOrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationKnowledgeOrders_OrdersOrderId",
                table: "MedicationKnowledgeOrders",
                column: "OrdersOrderId");
        }
    }
}
