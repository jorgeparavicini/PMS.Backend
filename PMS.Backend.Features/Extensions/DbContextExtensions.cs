using System.Threading.Tasks;
using Detached.Mappers.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace PMS.Backend.Features.Extensions;

public static class DbContextExtensions
{
    public static async Task<TResult> SaveSingle<TInput, TEntity, TResult>(DbContext dbContext, TInput input)
        where TEntity : class
    {
        TEntity entity = await dbContext.MapAsync<TEntity>(input);
    }
}
