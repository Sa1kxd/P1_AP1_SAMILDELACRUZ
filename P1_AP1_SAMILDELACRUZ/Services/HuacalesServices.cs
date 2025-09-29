using Microsoft.EntityFrameworkCore;
using P1_AP1_SAMILDELACRUZ.DAL;
using P1_AP1_SAMILDELACRUZ.Models;
using System.Linq.Expressions;

namespace P1_AP1_SAMILDELACRUZ.Services;

public class HuacalesServices(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Existe(int huacalesId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Huacales.AnyAsync(H => H.IdEntrada == huacalesId);
    }

    public async Task<bool> Insertar(EntradasHuacales huacales)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Huacales.Add(huacales);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(EntradasHuacales huacales)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(huacales);
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
        return await contexto.Huacales.Where(H => H.IdEntrada == HuacalesId).ExecuteDeleteAsync() > 0;
    }

    public async Task<EntradasHuacales> Buscar(int HuacalesId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Huacales.AsNoTracking().FirstOrDefaultAsync(H => H.IdEntrada == HuacalesId);
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
