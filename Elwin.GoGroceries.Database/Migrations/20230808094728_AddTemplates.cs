using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elwin.GoGroceries.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "grocery_list_templates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_grocery_list_templates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "grocery_list_template_product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    grocery_list_template_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    measurement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    measurement_quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_grocery_list_template_product", x => x.id);
                    table.ForeignKey(
                        name: "fk_grocery_list_template_product_grocery_list_templates_grocery_list_template_id",
                        column: x => x.grocery_list_template_id,
                        principalTable: "grocery_list_templates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_grocery_list_template_product_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_grocery_list_template_product_grocery_list_template_id",
                table: "grocery_list_template_product",
                column: "grocery_list_template_id");

            migrationBuilder.CreateIndex(
                name: "ix_grocery_list_template_product_product_id",
                table: "grocery_list_template_product",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grocery_list_template_product");

            migrationBuilder.DropTable(
                name: "grocery_list_templates");
        }
    }
}
