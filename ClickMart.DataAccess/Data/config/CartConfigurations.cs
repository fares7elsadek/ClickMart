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
    public class CartConfigurations : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("newid()");

            builder.ToTable("Carts");

            builder.Property(x => x.Quantity)
                .HasColumnType("int");

            builder.ToTable(t => t.HasCheckConstraint("CK_QuantityNotNegative", "[Quantity]>=0"));

            builder.Property(x => x.CouponDiscount)
                .HasColumnType("decimal")
                .HasPrecision(18, 2);
                

        }
    }
}
