using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaStock.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarFacturaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioAplicacionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FechaFactura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EconomatoId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Factura_AspNetUsers_UsuarioAplicacionId",
                        column: x => x.UsuarioAplicacionId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Factura_Economato_EconomatoId",
                        column: x => x.EconomatoId,
                        principalTable: "Economato",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Factura_EconomatoId",
                table: "Factura",
                column: "EconomatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_UsuarioAplicacionId",
                table: "Factura",
                column: "UsuarioAplicacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Factura");
        }
    }
}
