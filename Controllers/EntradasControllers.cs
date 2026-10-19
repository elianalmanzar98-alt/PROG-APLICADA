using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionLibros.DAL;
using GestionLibros.Models;

namespace GestionLibros.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EntradasController : ControllerBase, IDisposable
{
    private readonly Contexto _context;


    public EntradasController(IDbContextFactory<Contexto> dbFactory)
    {
        _context = dbFactory.CreateDbContext();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [HttpPost]
    public async Task<ActionResult<Entradas>> PostEntrada(Entradas entrada)
    {
        entrada.Total = entrada.EntradasDetalle.Sum(d => d.Cantidad);

        if (!EntradasExists(entrada.EntradaId))
        {
            // Crear
            await AfectarInventario(entrada.EntradasDetalle, 1);
            _context.Entradas.Add(entrada);
        }

        else
        {
            // Modificar
            var original = await _context.EntradasDetalles
                .AsNoTracking()

                .Where(d => d.EntradaId == entrada.EntradaId)
                .ToListAsync();

            await AfectarInventario(original, -1);
            await AfectarInventario(entrada.EntradasDetalle, 1);

            
            var idsActuales = entrada.EntradasDetalle.Select(d => d.Id).ToList();
            _context.EntradasDetalles.RemoveRange(original.Where(d => !idsActuales.Contains(d.Id)));

            _context.Update(entrada);
        }

        if (HayInventarioNegativo())
            return BadRequest(" se dejaria un libro con una cantidad en negativo");

        await _context.SaveChangesAsync();

        return Ok(entrada);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Entradas>> GetEntrada(int id)
    {
        if (_context.Entradas == null)
        {
            return NotFound();
        }


        var entrada = await _context.Entradas
            .Include(e => e.EntradasDetalle)
            .Where(e => e.EntradaId == id)
            .FirstOrDefaultAsync();

        if (entrada == null)
        {

            return NotFound();
        }

        return entrada;
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteEntrada(int id)
    {

        var entrada = await _context.Entradas
            .Include(e => e.EntradasDetalle)
            .FirstOrDefaultAsync(e => e.EntradaId == id);

        if (entrada == null)
        {
            return NotFound();
        }


        // Eliminar
        await AfectarInventario(entrada.EntradasDetalle, -1);

        if (HayInventarioNegativo())
            return BadRequest("La operacion dejaria un libro con cantidad negativa");


        _context.Entradas.Remove(entrada);
        await _context.SaveChangesAsync();

        return NoContent();
    }



    private bool EntradasExists(int id)
    {
        return _context.Entradas.Any(e => e.EntradaId == id);
    }



    private async Task AfectarInventario(IEnumerable<EntradasDetalle> detalle, int signo)
    {
        foreach (var d in detalle)
        {
            var libro = await _context.Libros.FindAsync(d.LibroId);
            if (libro != null)
                libro.CantidadDisponible += signo * d.Cantidad;
        }
    }


    private bool HayInventarioNegativo()
    {

        return _context.ChangeTracker.Entries<Libros>()
            .Any(e => e.Entity.CantidadDisponible < 0);
    }
}
