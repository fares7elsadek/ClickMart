using ClickMart.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClickMart.DataAccess.Data.config
{
	public class UserConfigurations : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.Property(u => u.FirstName)
				.HasColumnType("varchar")
				.HasMaxLength(128)
				.IsRequired();

            builder.Property(u => u.LastName)
                .HasColumnType("varchar")
                .HasMaxLength(128)
                .IsRequired();

            builder.HasMany(u => u.Address)
				.WithOne(u => u.User)
				.HasForeignKey(u => u.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
