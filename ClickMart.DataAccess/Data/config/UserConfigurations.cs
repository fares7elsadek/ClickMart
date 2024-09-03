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

			builder.HasMany(u => u.Addresses)
				.WithOne(u => u.User)
				.HasForeignKey(u => u.UserId)
				.OnDelete(DeleteBehavior.Cascade);


            builder.Property(u => u.avatar)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .HasDefaultValue("Images/User/Default/avatar.webp");

            builder.HasMany(x => x.Reviews)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Carts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
	}
}
