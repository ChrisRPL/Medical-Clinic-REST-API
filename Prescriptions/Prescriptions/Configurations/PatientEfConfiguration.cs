using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prescriptions.Models;

namespace Prescriptions.Configurations
{
    public class PatientEfConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
          
                builder.HasKey(e => e.IdPatient)
                    .HasName("Patient_pk");

                builder.ToTable("Patient");

                builder.Property(e => e.IdPatient).ValueGeneratedNever();

                builder.Property(e => e.Birthdate).HasColumnType("date");

                builder.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                var patients = new List<Patient>()
                {
                    new Patient()
                    {
                        IdPatient = 1, FirstName = "Jan", LastName = "Nowak", Birthdate = new DateTime(2000, 3, 15)
                    },
                    new Patient()
                    {
                        IdPatient = 2, FirstName = "Adam", LastName = "Nowak", Birthdate = new DateTime(2001, 6, 12)
                    },
                    new Patient()
                    {
                        IdPatient = 3, FirstName = "Piotr", LastName = "Nowak", Birthdate = new DateTime(2003, 10, 21)
                    },
                };

                builder.HasData(patients);
        }
    }
}