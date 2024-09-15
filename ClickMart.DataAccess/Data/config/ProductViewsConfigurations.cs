using ClickMart.Models.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.DataAccess.Data.config
{
    public class ProductViewsConfigurations : IEntityTypeConfiguration<ProductViews>
    {
        public void Configure(EntityTypeBuilder<ProductViews> builder)
        {
            builder.HasKey(p => new { p.UserId , p.ProductId });
            builder.ToTable(nameof(ProductViews));
        }
    }
}
