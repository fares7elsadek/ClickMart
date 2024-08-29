using ClickMart.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.DataAccess.Data.config
{
    public class GalleryConfgurations : IEntityTypeConfiguration<Galleries>
    {
        public void Configure(EntityTypeBuilder<Galleries> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable(nameof(Galleries));

            builder.Property(x => x.Id)
                .HasDefaultValueSql("newid()");


            builder.Property(x => x.ImagePath)
                .HasColumnType("varchar")
                .HasMaxLength(256);

            builder.Property(x => x.thumbnail)
                .HasColumnType("bit")
                .HasDefaultValue(false);

            builder.Property(x => x.displayOrder)
                .HasColumnType("smallint");

            builder.Property(x => x.CreatedAt)
                .HasColumnType("date")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.UpdatedAt)
                .HasColumnType("date")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
