using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Models.Data.config
{
    public class UserAddressConfigurations : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.HasKey(x => new { x.AddressId, x.UserId });

            builder.ToTable("UserAddresses");

            builder.Property(x => x.IsDefault)
                .HasColumnType("bit")
                .HasDefaultValue(false);
        }
    }
}
