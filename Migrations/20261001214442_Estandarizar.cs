using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionLibros.Migrations
{
    /// <inheritdoc />
    public partial class Estandarizar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestamo_Estudiantes_EstudianteId",
                table: "Prestamo");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestamo_Libros_LibroId",
                table: "Prestamo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prestamo",
                table: "Prestamo");

            migrationBuilder.RenameTable(
                name: "Prestamo",
                newName: "Prestamos");

            migrationBuilder.RenameIndex(
                name: "IX_Prestamo_LibroId",
                table: "Prestamos",
                newName: "IX_Prestamos_LibroId");

            migrationBuilder.RenameIndex(
                name: "IX_Prestamo_EstudianteId",
                table: "Prestamos",
                newName: "IX_Prestamos_EstudianteId");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Prestamos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prestamos",
                table: "Prestamos",
                column: "PrestamoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Estudiantes_EstudianteId",
                table: "Prestamos",
                column: "EstudianteId",
                principalTable: "Estudiantes",
                principalColumn: "EstudianteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Libros_LibroId",
                table: "Prestamos",
                column: "LibroId",
                principalTable: "Libros",
                principalColumn: "LibroId",
                onDelete: ReferentialAction.Cascade);

            // Le pone estado a los préstamos que ya existían
            migrationBuilder.Sql(@"UPDATE Prestamos
                SET Estado = CASE WHEN FechaDevolucion IS NULL THEN 'Prestado' ELSE 'Devuelto' END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Estudiantes_EstudianteId",
                table: "Prestamos");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Libros_LibroId",
                table: "Prestamos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prestamos",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Prestamos");

            migrationBuilder.RenameTable(
                name: "Prestamos",
                newName: "Prestamo");

            migrationBuilder.RenameIndex(
                name: "IX_Prestamos_LibroId",
                table: "Prestamo",
                newName: "IX_Prestamo_LibroId");

            migrationBuilder.RenameIndex(
                name: "IX_Prestamos_EstudianteId",
                table: "Prestamo",
                newName: "IX_Prestamo_EstudianteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prestamo",
                table: "Prestamo",
                column: "PrestamoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamo_Estudiantes_EstudianteId",
                table: "Prestamo",
                column: "EstudianteId",
                principalTable: "Estudiantes",
                principalColumn: "EstudianteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamo_Libros_LibroId",
                table: "Prestamo",
                column: "LibroId",
                principalTable: "Libros",
                principalColumn: "LibroId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}