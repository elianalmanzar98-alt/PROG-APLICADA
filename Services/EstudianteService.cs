using GestionLibros.DAL;
using GestionLibros.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionLibros.Services;

public class EstudianteService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> ExisteNombre(string nombre, int id = 0)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Estudiantes
            .AnyAsync(e => e.Nombres.ToLower() == nombre.ToLower() && e.EstudianteId != id);
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Estudiantes.AnyAsync(e => e.EstudianteId == id);
    }

    public async Task<bool> Guardar(Estudiante estudiante)
    {
        if (!await Existe(estudiante.EstudianteId))
            return await Insertar(estudiante);
        else
            return await Modificar(estudiante);
    }

    private async Task<bool> Insertar(Estudiante estudiante)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Estudiantes.Add(estudiante);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Estudiante estudiante)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(estudiante);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Estudiante>> Listar(Expression<Func<Estudiante, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Estudiantes.AsNoTracking().Where(criterio).ToListAsync();
    }
    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var estudiante = await contexto.Estudiantes.FindAsync(id);
        if (estudiante == null) return false;

        contexto.Estudiantes.Remove(estudiante);
        return await contexto.SaveChangesAsync() >0;
    }
}