

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionLibros.Models;

public class Entradas
{
    [Key]
    public int EntradaId {get; set;}

    public DateTime Fecha {get; set;} = DateTime.Today;

    [Required(ErrorMessage = "Concepto Obligatorio")]
    public string Concepto {get; set;} = string.Empty;

    public int Total{get; set;}
    [ForeignKey("EntradaId")]
    public ICollection<EntradasDetalle> EntradasDetalle {get; set;} = new List<EntradasDetalle>();

}