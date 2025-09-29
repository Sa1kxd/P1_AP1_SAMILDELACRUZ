namespace P1_AP1_SAMILDELACRUZ.Models;
using System.ComponentModel.DataAnnotations;

public class EntradasHuacales
{
    [Key]
    public int IdEntrada { get; set; }
    
    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "Fecha es requerida")]
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Nombre es requerido")]
    [StringLength(30, ErrorMessage = "El nombre no puede contener mas de 30 caracteres")]
    public String NombreCliente { get; set; }

    [Required(ErrorMessage ="Precion requerido")]
    public double Precio { get; set; }

}

