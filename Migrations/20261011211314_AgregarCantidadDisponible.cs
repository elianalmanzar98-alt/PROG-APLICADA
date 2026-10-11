using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionLibros.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCantidadDisponible : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CantidadDisponible",
                table: "Libros",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadDisponible",
                table: "Libros");
        }
    }
}
