using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClickMart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class imageurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "09a109c7-9bb9-43b1-84c7-e634a6730deb");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "907f93cf-57f1-4cf6-a00b-f456466669c5");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "ca3948ba-082d-4667-92c5-a6ec045cab62");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "da348224-5570-4e4f-a146-ad5ff62e39e8");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "fe4a5fdd-6887-4c3e-98cb-94402cf70a8c");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { "8353f715-a95c-4c46-a8aa-46eaaf3de867", "A68F7FAB-EC3A-4719-BD6D-A8DF974F2F0A", "A fast external SSD with 1TB storage capacity and USB 3.1 connectivity.", "", 120.00m, "External SSD" },
                    { "8cc58fd9-63a0-4997-b929-12f602b08665", "33ECBB18-84C1-4C7D-AB5D-84F3501F3059", "A high-quality wireless mouse with ergonomic design and long battery life.", "", 25.99m, "Wireless Mouse" },
                    { "b57d880d-e12f-4f77-846e-82acbc7bf14e", "A68F7FAB-EC3A-4719-BD6D-A8DF974F2F0A", "A durable mechanical keyboard with customizable RGB lighting and tactile feedback.", "", 75.50m, "Mechanical Keyboard" },
                    { "e5098b60-f31b-4c5c-a0a5-88b8243eef67", "33ECBB18-84C1-4C7D-AB5D-84F3501F3059", "A 27-inch 4K UHD monitor with vivid colors and sharp image quality.", "", 349.99m, "4K Monitor" },
                    { "f5855996-5248-4384-aaa4-526968e58820", "D54F4A29-8371-4AF5-B689-F2C1108F2295", "A compact USB-C hub with multiple ports including HDMI, USB 3.0, and Ethernet.", "", 39.99m, "USB-C Hub" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "8353f715-a95c-4c46-a8aa-46eaaf3de867");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "8cc58fd9-63a0-4997-b929-12f602b08665");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "b57d880d-e12f-4f77-846e-82acbc7bf14e");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "e5098b60-f31b-4c5c-a0a5-88b8243eef67");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "f5855996-5248-4384-aaa4-526968e58820");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Price", "Title" },
                values: new object[,]
                {
                    { "09a109c7-9bb9-43b1-84c7-e634a6730deb", "D54F4A29-8371-4AF5-B689-F2C1108F2295", "A compact USB-C hub with multiple ports including HDMI, USB 3.0, and Ethernet.", 39.99m, "USB-C Hub" },
                    { "907f93cf-57f1-4cf6-a00b-f456466669c5", "A68F7FAB-EC3A-4719-BD6D-A8DF974F2F0A", "A durable mechanical keyboard with customizable RGB lighting and tactile feedback.", 75.50m, "Mechanical Keyboard" },
                    { "ca3948ba-082d-4667-92c5-a6ec045cab62", "33ECBB18-84C1-4C7D-AB5D-84F3501F3059", "A high-quality wireless mouse with ergonomic design and long battery life.", 25.99m, "Wireless Mouse" },
                    { "da348224-5570-4e4f-a146-ad5ff62e39e8", "A68F7FAB-EC3A-4719-BD6D-A8DF974F2F0A", "A fast external SSD with 1TB storage capacity and USB 3.1 connectivity.", 120.00m, "External SSD" },
                    { "fe4a5fdd-6887-4c3e-98cb-94402cf70a8c", "33ECBB18-84C1-4C7D-AB5D-84F3501F3059", "A 27-inch 4K UHD monitor with vivid colors and sharp image quality.", 349.99m, "4K Monitor" }
                });
        }
    }
}
