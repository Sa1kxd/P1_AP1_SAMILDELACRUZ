namespace P1_AP1_SAMILDELACRUZ.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EntradasHuacales
{
    [Key]
    public int IdEntrada { get; set; }

    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0.")]
    public int Cantidad { get; set; }

    [Required(ErrorMessage = "Fecha es requerida")]
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Nombre es requerido")]
    [StringLength(30, ErrorMessage = "El nombre no puede contener mas de 30 caracteres")]
    public String NombreCliente { get; set; }

    [Required(ErrorMessage = "Precion requerido")]
    [Range(1, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
    public double Precio { get; set; }

    [InverseProperty("EntradaHuacal")]
    public virtual ICollection<EntradasHuacalesDetalle> EntradaHuacalDetalle { get; set; } = new List<EntradasHuacalesDetalle>();

}

