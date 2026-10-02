using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionLibros.Models;

public class Prestamos
{
    [Key]
    public int PrestamoId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Seleccione un estudiante")]
    public int EstudianteId { get; set; }

    [ForeignKey(nameof(EstudianteId))]
    public Estudiantes? Estudiante { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Seleccione un libro")]
    public int LibroId { get; set; }

    [ForeignKey(nameof(LibroId))]
    public Libros? Libro { get; set; }

    public DateTime FechaPrestamo { get; set; } = DateTime.Today;

    public DateTime? FechaDevolucion { get; set; }

    [Required(ErrorMessage = "Seleccione un estado")]
    public string Estado { get; set; } = string.Empty;
}