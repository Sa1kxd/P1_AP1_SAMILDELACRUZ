using Microsoft.EntityFrameworkCore;
using P1_AP1_SAMILDELACRUZ.Models;

namespace P1_AP1_SAMILDELACRUZ.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    public DbSet<EntradasHuacales> Registros { get; set; }
}
