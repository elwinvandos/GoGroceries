using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elwin.GoGroceries.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReworkWeightToMeasurement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "weight",
                table: "products",
                newName: "measurement_quantity");

            migrationBuilder.AddColumn<string>(
                name: "measurement",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "measurement",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "measurement_quantity",
                table: "products",
                newName: "weight");
        }
    }
}
