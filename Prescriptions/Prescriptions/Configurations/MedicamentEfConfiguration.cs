using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prescriptions.Models;

namespace Prescriptions.Configurations
{
    public class MedicamentEfConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.HasKey(e => e.IdMedicament)
                .HasName("Medicament_pk");

            builder.ToTable("Medicament");

            builder.Property(e => e.IdMedicament).ValueGeneratedNever();

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(100);

            var medicaments = new List<Medicament>
            {
                new() {IdMedicament = 1, Name = "Apap", Description = "Przeciwbólowy", Type = "Paracetamol"},
                new() {IdMedicament = 2, Name = "Cholinex", Description = "Na ból gardła", Type = "Przeciwbólowy"},
                new()
                {
                    IdMedicament = 3, Name = "Rutinoscorbin", Description = "Na wzmocnienie organizmu",
                    Type = "Witaminka"
                }
            };

            builder.HasData(medicaments);
        }
    }
}