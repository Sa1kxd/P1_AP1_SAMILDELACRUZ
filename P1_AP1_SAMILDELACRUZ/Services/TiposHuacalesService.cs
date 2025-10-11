using Microsoft.EntityFrameworkCore;
using P1_AP1_SAMILDELACRUZ.DAL;
using P1_AP1_SAMILDELACRUZ.Models;
using System.Linq.Expressions;

namespace P1_AP1_SAMILDELACRUZ.Services;

public class TiposHuacalesService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<TiposHuacales> Buscar(int tipoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposHuacales
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TipoId == tipoId);
    }
    public async Task<List<TiposHuacales>> Listar(Expression<Func<TiposHuacales, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposHuacales
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
