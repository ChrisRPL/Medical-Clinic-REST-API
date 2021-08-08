using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prescriptions.Models;

namespace Prescriptions.Configurations
{
    public class DoctorEfConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
        
                builder.HasKey(e => e.IdDoctor)
                    .HasName("Doctor_pk");

                builder.ToTable("Doctor");

                builder.Property(e => e.IdDoctor).ValueGeneratedNever();

                builder.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                var doctors = new List<Doctor>
                {
                    new Doctor() {IdDoctor = 1, FirstName = "Jan", LastName = "Kowalski", Email = "xyz@wp.pl"},
                    new Doctor() {IdDoctor = 2, FirstName = "Tomasz", LastName = "Kowalski", Email = "abc@wp.pl"},
                    new Doctor() {IdDoctor = 3, FirstName = "Andrzej", LastName = "Kowalski", Email = "def@wp.pl"}
                };

                builder.HasData(doctors);
        }
    }
}