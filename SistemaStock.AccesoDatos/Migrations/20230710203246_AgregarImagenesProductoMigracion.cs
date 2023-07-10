using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaStock.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarImagenesProductoMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagenUrlDos",
                table: "Producto",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagenUrlTres",
                table: "Producto",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenUrlDos",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "ImagenUrlTres",
                table: "Producto");
        }
    }
}
