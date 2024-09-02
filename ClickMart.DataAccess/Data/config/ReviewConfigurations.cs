using ClickMart.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.DataAccess.Data.config
{
    public class ReviewConfigurations : IEntityTypeConfiguration<Reviews>
    {
        public void Configure(EntityTypeBuilder<Reviews> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasDefaultValueSql("newid()");

            builder.Property(x => x.Title)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Description)
               .HasColumnType("text")
               .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.UpdatedAt)
               .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.Stars)
                .HasColumnType("int")
                .IsRequired();

            builder.ToTable(t => t.HasCheckConstraint("CK_Stars_NoNegative", "[Stars]>=1 AND [Stars]<=5"));

            builder.Property(x => x.Verified)
                .HasColumnType("bit")
                .HasDefaultValue(false);



        }
    }
}
