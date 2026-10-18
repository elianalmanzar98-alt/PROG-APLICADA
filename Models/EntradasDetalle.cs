

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionLibros.Models;

public class EntradasDetalle
{
    [Key]
    public int Id {get; set;}
    public int EntradaId{get; set;}
    public int LibroId {get; set;}
    public int Cantidad{ get; set;}
        
    
}