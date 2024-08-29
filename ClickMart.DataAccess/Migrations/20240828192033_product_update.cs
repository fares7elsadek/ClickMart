using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClickMart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class product_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "date",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPrice",
                table: "Products",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Products",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Products",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "newid()"),
                    AttributeName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "newid()"),
                    productId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ImagePath = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    thumbnail = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    displayOrder = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Galleries_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttributesValues",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "newid()"),
                    AttributeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AttributeValue = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Color = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributesValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributesValues_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attributes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AttributeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => new { x.ProductId, x.AttributeId });
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_discountPrice_NoNegative",
                table: "Products",
                sql: "[DiscountPrice]>=0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Quantity_NoNegative",
                table: "Products",
                sql: "[Quantity]>=0");

            migrationBuilder.CreateIndex(
                name: "IX_AttributesValues_AttributeId",
                table: "AttributesValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_productId",
                table: "Galleries",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_AttributeId",
                table: "ProductAttributes",
                column: "AttributeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributesValues");

            migrationBuilder.DropTable(
                name: "Galleries");

            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_discountPrice_NoNegative",
                table: "Products");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Quantity_NoNegative",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Products");

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
    }
}
