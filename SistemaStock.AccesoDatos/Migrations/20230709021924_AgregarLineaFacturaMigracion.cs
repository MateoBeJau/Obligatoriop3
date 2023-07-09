using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaStock.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarLineaFacturaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LineaFactura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacturaId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    StockAnterior = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaFactura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineaFactura_Factura_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Factura",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LineaFactura_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineaFactura_FacturaId",
                table: "LineaFactura",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_LineaFactura_ProductoId",
                table: "LineaFactura",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineaFactura");
        }
    }
}
