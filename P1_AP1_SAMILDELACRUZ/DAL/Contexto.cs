using Microsoft.EntityFrameworkCore;
using P1_AP1_SAMILDELACRUZ.Models;

namespace P1_AP1_SAMILDELACRUZ.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    public DbSet<EntradasHuacales> Huacales { get; set; }
    public DbSet<TiposHuacales> TiposHuacales { get; set; }
    public DbSet<EntradasHuacalesDetalle> EntradasHuacalesDetalle { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EntradasHuacales>()
    .HasMany(e => e.EntradaHuacalDetalle)
    .WithOne(d => d.EntradaHuacal)
    .HasForeignKey(d => d.IdEntrada)
    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TiposHuacales>()
    .HasMany(t => t.EntradaHuacalDetalle)
    .WithOne(d => d.TipoHuacal)
    .HasForeignKey(d => d.TipoId)
    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TiposHuacales>().HasData(
            new List<TiposHuacales>
            {
                new()
                {
                    TipoId = 1,
                    Descripcion = "Verde",
                    Existencia = 0
                },
                new()
                {
                    TipoId = 2,
                    Descripcion = "Rojo",
                    Existencia = 0
                }

            }
        );

    }
}
