using Microsoft.EntityFrameworkCore;
using P1_AP1_SAMILDELACRUZ.DAL;
using P1_AP1_SAMILDELACRUZ.Models;
using System.Linq.Expressions;

namespace P1_AP1_SAMILDELACRUZ.Services;

public class HuacalesServices(IDbContextFactory<Contexto> DbFactory)
{
    private enum TipoOperacion
    {
        Suma = 1,
        Resta = 2
    }


    public async Task<bool> Existe(int huacalesId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Huacales.AnyAsync(H => H.IdEntrada == huacalesId);
    }

    public async Task<bool> Insertar(EntradasHuacales huacales)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Huacales.Add(huacales);
        await AfectarExistencia(huacales.EntradaHuacalDetalle.ToArray(), TipoOperacion.Suma);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task AfectarExistencia(EntradasHuacalesDetalle[] detalles, TipoOperacion tipoOperacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        foreach (var item in detalles)
        {
            var tipoHuacal = await contexto.TiposHuacales.SingleAsync(t => t.TipoId == item.TipoId);

            if (tipoOperacion == TipoOperacion.Suma)
            {
                tipoHuacal.Existencia += item.Cantidad;
            }
            else
            {
                tipoHuacal.Existencia -= item.Cantidad;
            }
        }

        await contexto.SaveChangesAsync();
    }

    private async Task<bool> Modificar(EntradasHuacales huacal)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var detallesViejos = await contexto.EntradasHuacalesDetalle
            .AsNoTracking()
            .Where(e => e.IdEntrada == huacal.IdEntrada)
            .ToArrayAsync();

        await AfectarExistencia(detallesViejos, TipoOperacion.Resta);

        await contexto.EntradasHuacalesDetalle
            .Where(d => d.IdEntrada == huacal.IdEntrada)
            .ExecuteDeleteAsync();

        contexto.Huacales.Update(huacal);

        await AfectarExistencia(huacal.EntradaHuacalDetalle.ToArray(), TipoOperacion.Suma);

        return await contexto.SaveChangesAsync() > 0;
    }


    public async Task<bool> Guardar(EntradasHuacales huacales)
    {
        if (!await Existe(huacales.IdEntrada))
        {
            return await Insertar(huacales);
        }
        else
        {
            return await Modificar(huacales);
        }
    }

    public async Task<bool> Eliminar(int HuacalesId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var huacal = await contexto.Huacales
            .Include(e => e.EntradaHuacalDetalle)
            .FirstOrDefaultAsync(e => e.IdEntrada == HuacalesId);

        if (huacal == null) return false;

        await AfectarExistencia(huacal.EntradaHuacalDetalle.ToArray(), TipoOperacion.Resta);

        contexto.EntradasHuacalesDetalle.RemoveRange(huacal.EntradaHuacalDetalle);
        contexto.Huacales.Remove(huacal);

        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;
    }

    public async Task<EntradasHuacales> Buscar(int HuacalesId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Huacales.Include(e => e.EntradaHuacalDetalle)
                .ThenInclude(d => d.TipoHuacal)
                .AsNoTracking().FirstOrDefaultAsync(e => e.IdEntrada == HuacalesId);
    }

    public async Task<List<EntradasHuacales>> Listar(Expression<Func<EntradasHuacales, bool>> criterio)
    {
        using var context = await DbFactory.CreateDbContextAsync();
        return await context.Huacales.
            Where(criterio).
            AsNoTracking().
            ToListAsync();
    }
}
