using System.Linq.Expressions;
using GestionLibros.DAL;
using GestionLibros.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionLibros.Services;

public class LibrosService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Libros libro)
    {
        if (!await Existe(libro.LibroId))
            return await Insertar(libro);
        else
            return await Modificar(libro);
    }

    private async Task<bool> Existe(int libroId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Libros.AnyAsync(l => l.LibroId == libroId);
    }

    public async Task<bool> ExisteTitulo(string titulo, int libroId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Libros
            .AnyAsync(l => l.Titulo.ToLower() == titulo.ToLower() && l.LibroId != libroId);
    }

    private async Task<bool> Insertar(Libros libro)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Libros.Add(libro);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Libros libro)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(libro);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Libros?> Buscar(int libroId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Libros.FirstOrDefaultAsync(l => l.LibroId == libroId);
    }

    public async Task<bool> Eliminar(int libroId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Libros  .AsNoTracking()  .Where(l => l.LibroId == libroId) .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Libros>> Listar(Expression<Func<Libros, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Libros .Where(criterio)  .AsNoTracking() .ToListAsync();
    }
}