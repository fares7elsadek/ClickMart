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
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("newid()");

            builder.ToTable("Products");

            builder.Property(x => x.Title)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.ImageUrl)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.Price)
                .HasColumnType("decimal")
                .HasPrecision(18,2)
                .IsRequired();

            builder.ToTable(x => x.HasCheckConstraint("CK_Price_NonNegative", "[Price]>=0"));
            builder.HasData(GetProductData());
        }

        public List<Product> GetProductData()
        {
            return new List<Product>()
            {
                new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Wireless Mouse",
                    Description = "A high-quality wireless mouse with ergonomic design and long battery life.",
                    Price = 25.99m,
                    CategoryId= "33ECBB18-84C1-4C7D-AB5D-84F3501F3059",
                    ImageUrl = ""
                    
                },
                new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Mechanical Keyboard",
                    Description = "A durable mechanical keyboard with customizable RGB lighting and tactile feedback.",
                    Price = 75.50m,
                    CategoryId = "A68F7FAB-EC3A-4719-BD6D-A8DF974F2F0A",
                    ImageUrl = ""
                },
                new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "4K Monitor",
                    Description = "A 27-inch 4K UHD monitor with vivid colors and sharp image quality.",
                    Price = 349.99m,
                    CategoryId="33ECBB18-84C1-4C7D-AB5D-84F3501F3059",
                    ImageUrl = ""
                },
                new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "USB-C Hub",
                    Description = "A compact USB-C hub with multiple ports including HDMI, USB 3.0, and Ethernet.",
                    Price = 39.99m,
                    CategoryId="D54F4A29-8371-4AF5-B689-F2C1108F2295",
                    ImageUrl = ""
                },
                new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "External SSD",
                    Description = "A fast external SSD with 1TB storage capacity and USB 3.1 connectivity.",
                    Price = 120.00m,
                    CategoryId = "A68F7FAB-EC3A-4719-BD6D-A8DF974F2F0A",
                    ImageUrl = ""
                }
            };
        }

    }
}
