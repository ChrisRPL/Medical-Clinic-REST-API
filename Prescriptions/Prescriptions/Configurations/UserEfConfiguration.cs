using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prescriptions.Models;

namespace Prescriptions.Configurations
{
    public class UserEfConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.IdUser)
                .HasName("User_pk");
            
            builder.Property(e => e.IdUser).ValueGeneratedOnAdd().HasColumnType("int")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);;
            
            builder.Property<string>("Login")
                .HasColumnType("nvarchar(max)");

            builder.Property<string>("Password")
                .HasColumnType("nvarchar(max)");

            builder.Property<string>("RefreshToken")
                .HasColumnType("nvarchar(max)");

            builder.Property<DateTime?>("RefreshTokenExp")
                .HasColumnType("datetime2");

            builder.Property<string>("Salt")
                .HasColumnType("nvarchar(max)");

            builder.ToTable("User");
        }
    }
}