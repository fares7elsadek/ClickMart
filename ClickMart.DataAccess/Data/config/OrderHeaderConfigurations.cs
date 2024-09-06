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
    internal class OrderHeaderConfigurations : IEntityTypeConfiguration<OrderHeader>
    {
        public void Configure(EntityTypeBuilder<OrderHeader> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasDefaultValueSql("newid()");
            builder.ToTable("OrderHeaders");

            builder.Property(x => x.OrderDate)
                .HasDefaultValueSql("GETDATE()");

            builder.HasMany(x =>x.OrderDetails)
                .WithOne(x =>x.OrderHeader)
                .HasForeignKey(x =>x.OrderHeaderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ShippingMethod)
                .WithMany(x => x.OrderHeaders)
                .HasForeignKey(x => x.ShippingMethodId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
