using ClickMart.Models.Models;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.DataAccess.Data.config
{
    internal class CompanyConfigurations : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasDefaultValueSql("newid()");

            builder.ToTable("Companies");

            builder.Property(c => c.Name)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c => c.State)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c => c.City)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();


            builder.Property(c => c.StreetAddress)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c => c.PhoneNumber)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.PostalCode)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasMany(c => c.Users)
                .WithOne(c => c.Company)
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
