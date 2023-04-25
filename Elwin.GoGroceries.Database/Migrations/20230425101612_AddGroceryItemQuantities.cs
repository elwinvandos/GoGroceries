using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elwin.GoGroceries.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGroceryItemQuantities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "grocery_items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "weight",
                table: "grocery_items",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantity",
                table: "grocery_items");

            migrationBuilder.DropColumn(
                name: "weight",
                table: "grocery_items");
        }
    }
}
