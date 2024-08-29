using ClickMart.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.DataAccess.Data.config
{
    public class AttributeConfigurations : IEntityTypeConfiguration<Attributes>
    {
        public void Configure(EntityTypeBuilder<Attributes> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasDefaultValueSql("newid()");

            builder.ToTable(nameof(Attributes));

            builder.Property(x => x.AttributeName)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired(true);

            builder.Property(x => x.CreatedAt)
                .HasColumnType("date")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.UpdatedAt)
                .HasColumnType("date")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
