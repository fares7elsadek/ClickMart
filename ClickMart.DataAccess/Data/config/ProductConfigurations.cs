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

            

            builder.Property(x => x.Price)
                .HasColumnType("decimal")
                .HasPrecision(18,2)
                .IsRequired();

            builder.Property(x => x.DiscountPrice).
                HasColumnType("decimal")
                .HasPrecision(18, 2).IsRequired();

            builder.Property(x => x.Quantity).
                HasColumnType("int")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(x => x.ShortDescription).
                HasColumnType("text")
                .IsRequired();
                

            builder.Property(x => x.Published).
               HasColumnType("bit").
               HasDefaultValue(false)
               .IsRequired();

            builder.Property(x => x.OnSale).
             HasColumnType("bit").
             HasDefaultValue(false)
             .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnType("date")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.UpdatedAt)
                .HasColumnType("date")
                .HasDefaultValueSql("GETDATE()");

            builder.ToTable(t => t.HasCheckConstraint("CK_Quantity_NoNegative", "[Quantity]>=0"));
            builder.ToTable(t => t.HasCheckConstraint("CK_discountPrice_NoNegative", "[DiscountPrice]>=0"));
            builder.ToTable(x => x.HasCheckConstraint("CK_Price_NonNegative", "[Price]>=0"));

            builder.HasMany(x => x.Galleries)
                .WithOne(x => x.product)
                .HasForeignKey(x => x.productId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Attributes)
                .WithMany(x => x.Products)
                .UsingEntity<ProductAttributes>();

            builder.HasMany(x => x.Reviews)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x =>x.carts)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.OrderDetails)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Coupons)
                .WithMany(x => x.products)
                .UsingEntity<ProductCoupons>();

            builder.HasData(GetProductData());
        }

        public List<Product> GetProductData()
        {
            return new List<Product>()
            {
            };
        }

    }
}
