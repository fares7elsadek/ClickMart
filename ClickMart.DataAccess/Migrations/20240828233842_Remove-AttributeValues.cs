using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClickMart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAttributeValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributesValues");

            migrationBuilder.AddColumn<string>(
                name: "AttributeValue",
                table: "Attributes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttributeValue",
                table: "Attributes");

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

            migrationBuilder.CreateIndex(
                name: "IX_AttributesValues_AttributeId",
                table: "AttributesValues",
                column: "AttributeId");
        }
    }
}
