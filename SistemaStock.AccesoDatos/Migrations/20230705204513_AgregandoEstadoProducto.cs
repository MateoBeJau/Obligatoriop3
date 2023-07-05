using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaStock.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoEstadoProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Producto",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Producto");
        }
    }
}
