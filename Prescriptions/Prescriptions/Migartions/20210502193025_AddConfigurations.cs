using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prescriptions.Migartions
{
    public partial class AddConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "Doctor",
                new[] {"IdDoctor", "Email", "FirstName", "LastName"},
                new object[,]
                {
                    {1, "xyz@wp.pl", "Jan", "Kowalski"},
                    {2, "abc@wp.pl", "Tomasz", "Kowalski"},
                    {3, "def@wp.pl", "Andrzej", "Kowalski"}
                });

            migrationBuilder.InsertData(
                "Medicament",
                new[] {"IdMedicament", "Description", "Name", "Type"},
                new object[,]
                {
                    {1, "Przeciwbólowy", "Apap", "Paracetamol"},
                    {2, "Na ból gardła", "Cholinex", "Przeciwbólowy"},
                    {3, "Na wzmocnienie organizmu", "Rutinoscorbin", "Witaminka"}
                });

            migrationBuilder.InsertData(
                "Patient",
                new[] {"IdPatient", "Birthdate", "FirstName", "LastName"},
                new object[,]
                {
                    {1, new DateTime(2000, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jan", "Nowak"},
                    {2, new DateTime(2001, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adam", "Nowak"},
                    {3, new DateTime(2003, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Piotr", "Nowak"}
                });

            migrationBuilder.InsertData(
                "Prescription",
                new[] {"IdPrescription", "Date", "DueDate", "IdDoctor", "IdPatient"},
                new object[]
                {
                    1, new DateTime(2020, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2021, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1
                });

            migrationBuilder.InsertData(
                "Prescription",
                new[] {"IdPrescription", "Date", "DueDate", "IdDoctor", "IdPatient"},
                new object[]
                {
                    2, new DateTime(2021, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2021, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2
                });

            migrationBuilder.InsertData(
                "Prescription",
                new[] {"IdPrescription", "Date", "DueDate", "IdDoctor", "IdPatient"},
                new object[]
                {
                    3, new DateTime(2021, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2021, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3
                });

            migrationBuilder.InsertData(
                "Prescription_Medicament",
                new[] {"IdMedicament", "IdPrescription", "Details", "Dose"},
                new object[] {1, 1, "Nie brać po alkoholu", 2});

            migrationBuilder.InsertData(
                "Prescription_Medicament",
                new[] {"IdMedicament", "IdPrescription", "Details", "Dose"},
                new object[] {2, 2, "Brać rano, po obiedzie i wieczorem", 3});

            migrationBuilder.InsertData(
                "Prescription_Medicament",
                new[] {"IdMedicament", "IdPrescription", "Details", "Dose"},
                new object[] {3, 3, "Jedna rano", 1});
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Prescription_Medicament",
                new[] {"IdMedicament", "IdPrescription"},
                new object[] {1, 1});

            migrationBuilder.DeleteData(
                "Prescription_Medicament",
                new[] {"IdMedicament", "IdPrescription"},
                new object[] {2, 2});

            migrationBuilder.DeleteData(
                "Prescription_Medicament",
                new[] {"IdMedicament", "IdPrescription"},
                new object[] {3, 3});

            migrationBuilder.DeleteData(
                "Medicament",
                "IdMedicament",
                1);

            migrationBuilder.DeleteData(
                "Medicament",
                "IdMedicament",
                2);

            migrationBuilder.DeleteData(
                "Medicament",
                "IdMedicament",
                3);

            migrationBuilder.DeleteData(
                "Prescription",
                "IdPrescription",
                1);

            migrationBuilder.DeleteData(
                "Prescription",
                "IdPrescription",
                2);

            migrationBuilder.DeleteData(
                "Prescription",
                "IdPrescription",
                3);

            migrationBuilder.DeleteData(
                "Doctor",
                "IdDoctor",
                1);

            migrationBuilder.DeleteData(
                "Doctor",
                "IdDoctor",
                2);

            migrationBuilder.DeleteData(
                "Doctor",
                "IdDoctor",
                3);

            migrationBuilder.DeleteData(
                "Patient",
                "IdPatient",
                1);

            migrationBuilder.DeleteData(
                "Patient",
                "IdPatient",
                2);

            migrationBuilder.DeleteData(
                "Patient",
                "IdPatient",
                3);
        }
    }
}