using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClickMart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateuserdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "shippingMethodId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_shippingMethodId",
                table: "Users",
                column: "shippingMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ShippingMethod_shippingMethodId",
                table: "Users",
                column: "shippingMethodId",
                principalTable: "ShippingMethod",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ShippingMethod_shippingMethodId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_shippingMethodId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "shippingMethodId",
                table: "Users");
        }
    }
}
