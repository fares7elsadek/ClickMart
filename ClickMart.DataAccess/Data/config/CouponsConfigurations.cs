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
    internal class CouponsConfigurations : IEntityTypeConfiguration<Coupons>
    {
        public void Configure(EntityTypeBuilder<Coupons> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasDefaultValueSql("newid()");

            builder.ToTable(nameof(Coupons));

            builder.Property(x => x.code)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.couponDescription)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(x => x.discountValue)
                .HasColumnType("decimal")
                .HasPrecision(18, 2)
                .IsRequired(true);

            builder.Property(x => x.timesUsed)
                .HasColumnType("int");


            builder.Property(x => x.maxUsage)
                .HasColumnType("int")
                .IsRequired(true);

            builder.Property(x => x.couponStartDate)
                .HasColumnType("date")
                .IsRequired(true);

            builder.Property(x => x.couponEndDate)
                .HasColumnType("date")
                .IsRequired(true);

            builder.Property(x => x.CreatedAt)
                .HasColumnType("date")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.UpdatedAt)
                .HasColumnType("date");

            builder.HasMany(x => x.OrderHeaders)
                .WithOne(x => x.Coupons)
                .HasForeignKey(x => x.CouponId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
