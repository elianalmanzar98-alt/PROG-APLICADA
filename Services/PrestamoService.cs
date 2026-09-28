

using System.Linq.Expressions;
using GestionLibros.DAL;
using GestionLibros.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionLibros.Services;
public class PrestamoService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> LibroDisponible(int LibroId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return !await contexto.Prestamo.AnyAsync(p => p.LibroId == LibroId && p.FechaDevolucion == null);
    }

    public async Task <bool> Prestar(int LibroId, int EstudianteId)
    {
        if (!await LibroDisponible(LibroId)) 
        return false;

        await using var contexto = await DbFactory.CreateDbContextAsync();
        var Prestamo = new Prestamo
        {
            LibroId = LibroId,
            EstudianteId = EstudianteId,
            FechaPrestamo = DateTime.Now
        };
        contexto.Prestamo.Add(Prestamo);
        return await contexto.SaveChangesAsync() >0;
    }

    public async Task<bool> Devolver(int PrestamoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var Prestamo = await contexto.Prestamo.FindAsync(PrestamoId);
        if(Prestamo == null || Prestamo.FechaDevolucion != null) return false;

        Prestamo.FechaDevolucion = DateTime.Now;
        contexto.Prestamo.Update(Prestamo);
        return await contexto.SaveChangesAsync()>0;
    }

    public async Task<bool> Eliminar(int PrestamoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var Prestamo= await contexto.Prestamo.FindAsync(PrestamoId);
        if (Prestamo == null)
        return false;
        
        contexto.Prestamo.Remove(Prestamo);
        return await contexto.SaveChangesAsync() >0;
    }
    public async Task<List<Prestamo>> Listar(Expression<Func<Prestamo, bool>> criterio)
    {
        await using var contexto =  await DbFactory.CreateDbContextAsync();
        return await contexto.Prestamo.Include(p => p.libros).Include(p => p.estudiante)
        .AsNoTracking().Where(criterio).OrderByDescending(p => p.FechaPrestamo).ToListAsync();
    }

}