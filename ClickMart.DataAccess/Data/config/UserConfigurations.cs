using ClickMart.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClickMart.DataAccess.Data.config
{
	public class UserConfigurations : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasMany(u => u.Address)
				.WithOne(u => u.User)
				.HasForeignKey(u => u.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
