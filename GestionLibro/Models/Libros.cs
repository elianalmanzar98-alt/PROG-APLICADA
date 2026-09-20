using System.ComponentModel.DataAnnotations;

namespace GestionLibros.Models;

public class Libros
{
    [Key]
    public int LibroId { get; set; }

    [Required(ErrorMessage = "El Ttulo debe de ser  obligatorio")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El Autor obligatorio")]
    public string Autor { get; set; } = string.Empty;

    [Required(ErrorMessage = "El Año de Publicacion debe de ser eobligatorio")]
    public int AnoPublicacion { get; set; }
}


