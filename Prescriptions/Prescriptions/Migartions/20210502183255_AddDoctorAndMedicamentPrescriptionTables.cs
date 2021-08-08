using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prescriptions.Migartions
{
    public partial class AddDoctorAndMedicamentPrescriptionTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Doctor",
                table => new
                {
                    IdDoctor = table.Column<int>("int", nullable: false),
                    FirstName = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table => { table.PrimaryKey("Doctor_pk", x => x.IdDoctor); });

            migrationBuilder.CreateTable(
                "Medicament",
                table => new
                {
                    IdMedicament = table.Column<int>("int", nullable: false),
                    Name = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table => { table.PrimaryKey("Medicament_pk", x => x.IdMedicament); });

            migrationBuilder.CreateTable(
                "Patient",
                table => new
                {
                    IdPatient = table.Column<int>("int", nullable: false),
                    FirstName = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false),
                    Birthdate = table.Column<DateTime>("date", nullable: false)
                },
                constraints: table => { table.PrimaryKey("Patient_pk", x => x.IdPatient); });

            migrationBuilder.CreateTable(
                "Prescription",
                table => new
                {
                    IdPrescription = table.Column<int>("int", nullable: false),
                    Date = table.Column<DateTime>("date", nullable: false),
                    DueDate = table.Column<DateTime>("date", nullable: false),
                    IdPatient = table.Column<int>("int", nullable: false),
                    IdDoctor = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Prescription_pk", x => x.IdPrescription);
                    table.ForeignKey(
                        "Prescription_Doctor",
                        x => x.IdDoctor,
                        "Doctor",
                        "IdDoctor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "Prescription_Patient",
                        x => x.IdPatient,
                        "Patient",
                        "IdPatient",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Prescription_Medicament",
                table => new
                {
                    IdMedicament = table.Column<int>("int", nullable: false),
                    IdPrescription = table.Column<int>("int", nullable: false),
                    Dose = table.Column<int>("int", nullable: false),
                    Details = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Prescription_Medicament_pk", x => new {x.IdMedicament, x.IdPrescription});
                    table.ForeignKey(
                        "Prescription_Medicament_Medicament",
                        x => x.IdMedicament,
                        "Medicament",
                        "IdMedicament",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "Prescription_Medicament_Prescription",
                        x => x.IdPrescription,
                        "Prescription",
                        "IdPrescription",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Prescription_IdDoctor",
                "Prescription",
                "IdDoctor");

            migrationBuilder.CreateIndex(
                "IX_Prescription_IdPatient",
                "Prescription",
                "IdPatient");

            migrationBuilder.CreateIndex(
                "IX_Prescription_Medicament_IdPrescription",
                "Prescription_Medicament",
                "IdPrescription");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Prescription_Medicament");

            migrationBuilder.DropTable(
                "Medicament");

            migrationBuilder.DropTable(
                "Prescription");

            migrationBuilder.DropTable(
                "Doctor");

            migrationBuilder.DropTable(
                "Patient");
        }
    }
}