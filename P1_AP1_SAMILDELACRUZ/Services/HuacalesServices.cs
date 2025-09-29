using Microsoft.EntityFrameworkCore;
using P1_AP1_SAMILDELACRUZ.DAL;
using P1_AP1_SAMILDELACRUZ.Models;
using System.Linq.Expressions;

namespace P1_AP1_SAMILDELACRUZ.Services;

public class HuacalesServices(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<List<EntradasHuacales>> Listar(Expression<Func<EntradasHuacales, bool>> criterio) 
    {
        using var context = await DbFactory.CreateDbContextAsync();
        return await context.Registros.
            Where(criterio).
            AsNoTracking().
            ToListAsync();
    }
}
