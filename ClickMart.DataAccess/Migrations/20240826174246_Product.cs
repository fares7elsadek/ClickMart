using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClickMart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "newid()"),
                    Title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.CheckConstraint("CK_Price_NonNegative", "[Price]>=0");
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Price", "Title" },
                values: new object[,]
                {
                    { "5800f086-4cab-4198-8d5c-488a703f5646", "A compact USB-C hub with multiple ports including HDMI, USB 3.0, and Ethernet.", 39.99m, "USB-C Hub" },
                    { "a70fb8c1-fba3-4901-9eae-7251c292766f", "A durable mechanical keyboard with customizable RGB lighting and tactile feedback.", 75.50m, "Mechanical Keyboard" },
                    { "c4392501-aee2-4b3d-9999-d0859e622515", "A fast external SSD with 1TB storage capacity and USB 3.1 connectivity.", 120.00m, "External SSD" },
                    { "f0395e28-598d-4363-a831-34cab1ddc7b4", "A high-quality wireless mouse with ergonomic design and long battery life.", 25.99m, "Wireless Mouse" },
                    { "fbb57afd-501c-4a34-a498-a12fd72f824f", "A 27-inch 4K UHD monitor with vivid colors and sharp image quality.", 349.99m, "4K Monitor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
