using ClickMart.DataAccess.Data;
using ClickMart.DataAccess.Repository;
using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using ClickMart.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Text.Json.Serialization;

namespace ClickMart
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			// DbContext
			builder.Services.AddDbContext<AppDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
			});
			builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(100);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});
			builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
			builder.Services.AddRazorPages();


			builder.Services.AddIdentity<User,IdentityRole>()
				.AddEntityFrameworkStores<AppDbContext>()
				.AddDefaultUI()
				.AddDefaultTokenProviders();

			builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

			builder.Services.ConfigureApplicationCookie(option =>
			{
				option.LogoutPath = "/Identity/Account/Login";
				option.LogoutPath = "/Identity/Account/logout";
				option.AccessDeniedPath = "/Identity/Account/AccessDenied";
			});


            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
			app.UseRouting();

			app.UseAuthorization();
			app.UseSession();
			app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

            app.Run();
		}
	}
}
