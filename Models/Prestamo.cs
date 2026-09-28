

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionLibros.Models;

public class Prestamo
{
    [Key]
    public int PrestamoId{get; set;}

    [Required(ErrorMessage ="Seleccione un libro")]
    public int LibroId{get; set;}

    [ForeignKey(nameof(LibroId))]
    public Libros? libros {get; set;}

    [Required(ErrorMessage ="Seleccione un estudiante")]
    public int EstudianteId{get; set;}
    
    [ForeignKey(nameof(EstudianteId))]
    public Estudiante? estudiante{get; set;}

    public DateTime FechaPrestamo { get; set;} = DateTime.Now;

    public DateTime? FechaDevolucion {get; set;}
}