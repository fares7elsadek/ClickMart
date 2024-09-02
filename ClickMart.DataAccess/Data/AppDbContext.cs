using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using ClickMart.DataAccess.Data.config;
using ClickMart.Models.Models;
using Microsoft.AspNetCore.Identity;

namespace ClickMart.DataAccess.Data
{
	public class AppDbContext : IdentityDbContext<User>
	{

		public DbSet<Address> Address { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Galleries> Galleries { get; set; }
		public DbSet<Attributes> Attributes { get; set; }

		public DbSet<Reviews> Reviews { get; set; }
		public AppDbContext(DbContextOptions options) : base(options)
		{

		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(typeof(UserConfigurations)
				.Assembly);
			builder.Entity<User>().ToTable("Users");
			builder.Entity<IdentityRole>().ToTable("Roles");
			builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
			builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
			builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
			builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
			builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        }
	}
}
