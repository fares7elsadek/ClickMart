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
    internal class ShippingMethodConfigurations : IEntityTypeConfiguration<ShippingMethod>
    {
        public void Configure(EntityTypeBuilder<ShippingMethod> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasDefaultValueSql("newid()");

            builder.ToTable("ShippingMethod");

            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(255);

            builder.Property(x => x.Description)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.Price)
                .HasColumnType("decimal")
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Default)
                .HasColumnType("bit")
                .HasDefaultValue(false);

            //builder.HasData(LoadData());
        }

        public List<ShippingMethod> LoadData()
        {
            return new List<ShippingMethod>()
            {
                new ShippingMethod
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Standard Shipping",
                    Price = 5.99M,
                    Description = "Delivery within 5-7 business days."
                },
                new ShippingMethod
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Express Shipping",
                    Price = 12.99M,
                    Description = "Faster delivery within 2-3 business days."
                },
                new ShippingMethod
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Overnight Shipping",
                    Price = 24.99M,
                    Description = "Next-day delivery for urgent orders."
                }
            };
        }

    }
}
