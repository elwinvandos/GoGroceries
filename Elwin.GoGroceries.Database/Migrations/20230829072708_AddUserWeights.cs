using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elwin.GoGroceries.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserWeights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_weight",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "user_weight",
                table: "categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_weight",
                table: "products");

            migrationBuilder.DropColumn(
                name: "user_weight",
                table: "categories");
        }
    }
}
