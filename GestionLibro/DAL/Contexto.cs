using Microsoft.EntityFrameworkCore;
using GestionLibros.Models;

namespace GestionLibros.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
        
        Database.EnsureCreated();
    }

    public DbSet<Libros> Libros { get; set; }

    public DbSet<Estudiante> Estudiantes {get; set;}
}