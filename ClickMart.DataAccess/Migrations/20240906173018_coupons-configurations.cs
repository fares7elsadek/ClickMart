using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClickMart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class couponsconfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CouponId",
                table: "OrderHeaders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "newid()"),
                    code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    couponDescription = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    discountValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    timesUsed = table.Column<int>(type: "int", nullable: true),
                    maxUsage = table.Column<int>(type: "int", nullable: false),
                    couponStartDate = table.Column<DateTime>(type: "date", nullable: false),
                    couponEndDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CouponsProduct",
                columns: table => new
                {
                    CouponsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    productsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponsProduct", x => new { x.CouponsId, x.productsId });
                    table.ForeignKey(
                        name: "FK_CouponsProduct_Coupons_CouponsId",
                        column: x => x.CouponsId,
                        principalTable: "Coupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CouponsProduct_Products_productsId",
                        column: x => x.productsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCoupons",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CouponId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CouponsId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCoupons", x => new { x.CouponId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductCoupons_Coupons_CouponsId",
                        column: x => x.CouponsId,
                        principalTable: "Coupons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductCoupons_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_CouponId",
                table: "OrderHeaders",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponsProduct_productsId",
                table: "CouponsProduct",
                column: "productsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCoupons_CouponsId",
                table: "ProductCoupons",
                column: "CouponsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCoupons_ProductId",
                table: "ProductCoupons",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_Coupons_CouponId",
                table: "OrderHeaders",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_Coupons_CouponId",
                table: "OrderHeaders");

            migrationBuilder.DropTable(
                name: "CouponsProduct");

            migrationBuilder.DropTable(
                name: "ProductCoupons");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeaders_CouponId",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "OrderHeaders");
        }
    }
}
