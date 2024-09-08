using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClickMart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addmessageorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerMessage",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerMessage",
                table: "OrderHeaders");
        }
    }
}
