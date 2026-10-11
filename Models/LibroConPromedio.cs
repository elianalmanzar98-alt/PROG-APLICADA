namespace GestionLibros.Models;

public class LibroConPromedio
{
    public Libros Libro {get; set;} = new();
    public int VecesPrestado{get; set;}
    public double PromedioDias{get; set;}
}