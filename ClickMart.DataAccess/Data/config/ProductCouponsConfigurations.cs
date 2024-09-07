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
    internal class ProductCouponsConfigurations : IEntityTypeConfiguration<ProductCoupons>
    {
        public void Configure(EntityTypeBuilder<ProductCoupons> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.CouponId });
            builder.ToTable(nameof(ProductCoupons));
        }
    }
}
