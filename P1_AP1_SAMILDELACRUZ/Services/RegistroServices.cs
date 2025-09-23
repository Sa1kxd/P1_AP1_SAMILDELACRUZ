using Microsoft.EntityFrameworkCore;
using P1_AP1_SAMILDELACRUZ.DAL;
using P1_AP1_SAMILDELACRUZ.Models;
using System.Linq.Expressions;

namespace P1_AP1_SAMILDELACRUZ.Services;

public class RegistroServices(IDbContextFactory<Contexto> factory)
{
    public async Task<List<Registro>> Listar(Expression<Func<Registro, bool>> criterio) 
    {
        using var context = await factory.CreateDbContextAsync();
        return await context.Registros.
            Where(criterio).
            AsNoTracking().
            ToListAsync();
    }
}
