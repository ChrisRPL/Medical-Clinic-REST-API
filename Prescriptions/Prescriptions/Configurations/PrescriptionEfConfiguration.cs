using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prescriptions.Models;

namespace Prescriptions.Configurations
{
    public class PrescriptionEfConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(e => e.IdPrescription)
                .HasName("Prescription_pk");

            builder.ToTable("Prescription");

            builder.Property(e => e.IdPrescription).ValueGeneratedNever();

            builder.Property(e => e.Date).HasColumnType("date");

            builder.Property(e => e.DueDate).HasColumnType("date");

            builder.HasOne(d => d.IdDoctorNavigation)
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Prescription_Doctor");

            builder.HasOne(d => d.IdPatientNavigation)
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Prescription_Patient");

            var prescriptions = new List<Prescription>
            {
                new()
                {
                    IdPrescription = 1,
                    Date = new DateTime(2020, 12, 20),
                    DueDate = new DateTime(2021, 1, 20),
                    IdPatient = 1,
                    IdDoctor = 1
                },
                new()
                {
                    IdPrescription = 2,
                    Date = new DateTime(2021, 1, 12),
                    DueDate = new DateTime(2021, 2, 12),
                    IdPatient = 2,
                    IdDoctor = 2
                },
                new()
                {
                    IdPrescription = 3,
                    Date = new DateTime(2021, 3, 20),
                    DueDate = new DateTime(2021, 4, 20),
                    IdPatient = 3,
                    IdDoctor = 3
                }
            };

            builder.HasData(prescriptions);
        }
    }
}