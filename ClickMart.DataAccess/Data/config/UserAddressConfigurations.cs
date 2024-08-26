using ClickMart.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClickMart.DataAccess.Data.config
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
