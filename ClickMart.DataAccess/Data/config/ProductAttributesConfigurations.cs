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
    public class ProductAttributesConfigurations : IEntityTypeConfiguration<ProductAttributes>
    {
        public void Configure(EntityTypeBuilder<ProductAttributes> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.AttributeId });
            builder.ToTable(nameof(ProductAttributes));
        }
    }
}
