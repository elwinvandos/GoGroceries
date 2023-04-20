using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elwin.GoGroceries.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "grocery_lists",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_grocery_lists", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "grocery_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    grocery_list_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_grocery_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_grocery_items_grocery_lists_grocery_list_id",
                        column: x => x.grocery_list_id,
                        principalTable: "grocery_lists",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_grocery_items_grocery_list_id",
                table: "grocery_items",
                column: "grocery_list_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grocery_items");

            migrationBuilder.DropTable(
                name: "grocery_lists");
        }
    }
}
