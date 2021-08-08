using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prescriptions.Models;

namespace Prescriptions.Configurations
{
    public class PrescriptionMedicamentEfConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
    {
        public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
        {
            builder.HasKey(e => new {e.IdMedicament, e.IdPrescription})
                .HasName("Prescription_Medicament_pk");

            builder.ToTable("Prescription_Medicament");

            builder.Property(e => e.Details)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(d => d.IdMedicamentNavigation)
                .WithMany(p => p.PrescriptionMedicaments)
                .HasForeignKey(d => d.IdMedicament)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Prescription_Medicament_Medicament");

            builder.HasOne(d => d.IdPrescriptionNavigation)
                .WithMany(p => p.PrescriptionMedicaments)
                .HasForeignKey(d => d.IdPrescription)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Prescription_Medicament_Prescription");

            var prescriptionsMedicaments = new List<PrescriptionMedicament>
            {
                new() {IdMedicament = 1, IdPrescription = 1, Dose = 2, Details = "Nie brać po alkoholu"},
                new() {IdMedicament = 2, IdPrescription = 2, Dose = 3, Details = "Brać rano, po obiedzie i wieczorem"},
                new() {IdMedicament = 3, IdPrescription = 3, Dose = 1, Details = "Jedna rano"}
            };

            builder.HasData(prescriptionsMedicaments);
        }
    }
}