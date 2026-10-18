using Microsoft.EntityFrameworkCore;
using GestionLibros.Models;

namespace GestionLibros.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Libros> Libros { get; set; }
    public DbSet<Estudiantes> Estudiantes { get; set; }
    public DbSet<Prestamos> Prestamos { get; set; }
    public DbSet<Entradas> Entradas{get; set;}
    public DbSet<EntradasDetalle> EntradasDetalles {get; set;}
}