using System.Linq.Expressions;
using GestionLibros.DAL;
using GestionLibros.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionLibros.Services;

public class PrestamosService(IDbContextFactory<Contexto> DbFactory) : IServiceProvider<Prestamos>
{
    public async Task<bool> Guardar(Prestamos prestamo)
    {
        if (!await Existe(prestamo.PrestamoId))
            return await Insertar(prestamo);
        else
            return await Modificar(prestamo);
    }

    private async Task<bool> Existe(int prestamoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prestamos.AnyAsync(p => p.PrestamoId == prestamoId);
    }

    // Validación: el libro no puede estar prestado a otra persona
    public async Task<bool> LibroPrestado(int libroId, int prestamoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prestamos.AnyAsync(p =>
            p.LibroId == libroId && p.Estado == "Prestado" && p.PrestamoId != prestamoId);
    }

    private async Task<bool> Insertar(Prestamos prestamo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Prestamos.Add(prestamo);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Prestamos prestamo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        prestamo.Estudiante = null; // evita que Update toque también al estudiante y al libro
        prestamo.Libro = null;
        contexto.Update(prestamo);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Prestamos?> Buscar(int prestamoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prestamos
            .Include(p => p.Estudiante)
            .Include(p => p.Libro)
            .FirstOrDefaultAsync(p => p.PrestamoId == prestamoId);
    }

    public async Task<bool> Eliminar(int prestamoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prestamos
            .AsNoTracking()
            .Where(p => p.PrestamoId == prestamoId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Prestamos>> Listar(Expression<Func<Prestamos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prestamos
            .Include(p => p.Estudiante)
            .Include(p => p.Libro)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}