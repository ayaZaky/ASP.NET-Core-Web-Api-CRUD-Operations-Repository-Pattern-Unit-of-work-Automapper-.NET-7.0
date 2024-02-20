using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emc2.EF.Migrations
{
    /// <inheritdoc />
    public partial class new1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productTechnologies");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "technologies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_technologies_ProductId",
                table: "technologies",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_technologies_products_ProductId",
                table: "technologies",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_technologies_products_ProductId",
                table: "technologies");

            migrationBuilder.DropIndex(
                name: "IX_technologies_ProductId",
                table: "technologies");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "technologies");

            migrationBuilder.CreateTable(
                name: "productTechnologies",
                columns: table => new
                {
                    ProductTechnologyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    TechnologyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productTechnologies", x => x.ProductTechnologyId);
                    table.ForeignKey(
                        name: "FK_productTechnologies_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productTechnologies_technologies_TechnologyId",
                        column: x => x.TechnologyId,
                        principalTable: "technologies",
                        principalColumn: "TechnologyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productTechnologies_ProductId",
                table: "productTechnologies",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_productTechnologies_TechnologyId",
                table: "productTechnologies",
                column: "TechnologyId");
        }
    }
}
