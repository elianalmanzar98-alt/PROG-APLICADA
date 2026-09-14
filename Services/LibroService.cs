using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using GestionLibros.DAL;
using GestionLibros.Models;

namespace GestionLibros.Services;

public class LibroService
{
    private readonly Contexto _contexto;

    public LibroService(Contexto contexto)
    {
        _contexto = contexto;
    }

    public bool Existe(int id)
    {

        return _contexto.Libros.Any(l => l.LibroId == id);
    }
    public bool ExisteTitulo(string titulo, int idActual)
    {
       
        return _contexto.Libros.Any(l => l.Titulo.ToLower() == titulo.ToLower() && l.LibroId != idActual);
    }
    private bool Insertar(Libros libro)
    {
        _contexto.Libros.Add(libro);
        return _contexto.SaveChanges() > 0;
    }

    private bool Modificar(Libros libro)
    {
        
        _contexto.ChangeTracker.Clear();

        _contexto.Libros.Update(libro);
        return _contexto.SaveChanges() > 0;
    }

    public bool Guardar(Libros libro)
    {
        if (!Existe(libro.LibroId))
            return Insertar(libro);
        else
            return Modificar(libro);
    }

   
    public Libros? Obtener(int id)
    {
        return _contexto.Libros.AsNoTracking().FirstOrDefault(l => l.LibroId == id);
    }


    public bool Eliminar(int id)
    {
        var libro = _contexto.Libros.Find(id);
        if (libro != null)
        {
            _contexto.Libros.Remove(libro);
            return _contexto.SaveChanges() > 0;
        }
        return false;
    }

    
    public List<Libros> Listar(Expression<Func<Libros, bool>> criterio)
    {
        return _contexto.Libros.AsNoTracking().Where(criterio).ToList();
    }
}