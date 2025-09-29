namespace P1_AP1_SAMILDELACRUZ.Models;
using System.ComponentModel.DataAnnotations;

public class EntradasHuacales
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]

    public string Nombre { get; set; }
}

