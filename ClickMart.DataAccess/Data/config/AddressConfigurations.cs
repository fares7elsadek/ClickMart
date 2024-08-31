using ClickMart.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClickMart.DataAccess.Data.config
{
	public class AddressConfigurations : IEntityTypeConfiguration<Address>
	{
		public void Configure(EntityTypeBuilder<Address> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(c => c.Id)
				.HasDefaultValueSql("newid()");

			builder.ToTable("Addresses");

			builder.Property(x => x.UnitNumber)
				.HasColumnType("varchar")
				.HasMaxLength(10);

			builder.Property(x => x.StreetNumber)
				.HasColumnType("varchar")
				.HasMaxLength(10);

			builder.Property(x => x.AddressLine1)
				.HasColumnType("varchar")
				.HasMaxLength(255);

			builder.Property(x => x.AddressLine2)
				.HasColumnType("varchar")
				.HasMaxLength(255);

			builder.Property(x => x.City)
				.HasColumnType("varchar")
				.HasMaxLength(255);

			builder.Property(x => x.Region)
				.HasColumnType("varchar")
				.HasMaxLength(255);

			builder.Property(x => x.PostalCode)
				.HasColumnType("varchar")
				.HasMaxLength(5);
		}
	}
}
