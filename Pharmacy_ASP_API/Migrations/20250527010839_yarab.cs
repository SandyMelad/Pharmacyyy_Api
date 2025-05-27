using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy_ASP_API.Migrations
{
    /// <inheritdoc />
    public partial class yarab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PatientID",
                table: "MedicationRequests",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationRequests_PatientID",
                table: "MedicationRequests",
                column: "PatientID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationRequests_Patients_PatientID",
                table: "MedicationRequests",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationRequests_Patients_PatientID",
                table: "MedicationRequests");

            migrationBuilder.DropIndex(
                name: "IX_MedicationRequests_PatientID",
                table: "MedicationRequests");

            migrationBuilder.DropColumn(
                name: "PatientID",
                table: "MedicationRequests");
        }
    }
}
