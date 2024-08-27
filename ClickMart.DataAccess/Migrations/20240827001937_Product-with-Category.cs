using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClickMart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ProductwithCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "5800f086-4cab-4198-8d5c-488a703f5646");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "a70fb8c1-fba3-4901-9eae-7251c292766f");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "c4392501-aee2-4b3d-9999-d0859e622515");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "f0395e28-598d-4363-a831-34cab1ddc7b4");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "fbb57afd-501c-4a34-a498-a12fd72f824f");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

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

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

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
    }
}
