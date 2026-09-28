

using System.ComponentModel.DataAnnotations;

namespace GestionLibros.Models;

public class Estudiante {
    [Key]
    public int EstudianteId {get; set;}


    [Required(ErrorMessage = "Nombre debe ser obligatorio")]
    public string Nombres {get; set;} = string.Empty;


    [Required(ErrorMessage = "La direccion debe ser obligatoria")]
    public string Direccion {get; set;} = string.Empty;


    [Required(ErrorMessage = "el email es obligatorio")]
    [EmailAddress(ErrorMessage = "el formato del email no es valido")]
    public string Email {get; set;} = string.Empty;


    [Required(ErrorMessage ="fecha de nacimiento obligatoria")]
    public DateTime? FechaNacimiento {get; set;}


}