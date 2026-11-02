using System.Collections.Generic;

namespace RegistroPrestamos.Data;

public partial class Libro
{
    public int LibroId { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Autor { get; set; } = string.Empty;

    public int AnoPublicacion { get; set; }

    public int Cantidad { get; set; }

    public virtual ICollection<RegistroPrestamos.Data.EntradasDetalle> EntradasDetalles { get; set; } = new List<RegistroPrestamos.Data.EntradasDetalle>();

    public virtual ICollection<RegistroPrestamos.Data.EstudianteDetalle> EstudianteDetalles { get; set; } = new List<RegistroPrestamos.Data.EstudianteDetalle>();

    public virtual ICollection<RegistroPrestamos.Data.PrestamoDetalle> PrestamoDetalles { get; set; } = new List<RegistroPrestamos.Data.PrestamoDetalle>();

    public virtual ICollection<RegistroPrestamos.Data.Prestamo> Prestamos { get; set; } = new List<RegistroPrestamos.Data.Prestamo>();
}