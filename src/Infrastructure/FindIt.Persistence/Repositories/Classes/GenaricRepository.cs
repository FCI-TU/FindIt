using FindIt.Domain.Common;
using FindIt.Domain.Interfaces;
using FindIt.Domain.Specifications;
using FindIt.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FindIt.Persistence.Repositories.Classes;
public class GenericRepository<T>(StoreDbContext dbContext) : IGenericRepository<T> where T : class
{
    public async Task<IReadOnlyList<T>> GetAllAsync(bool withNoTracking = true)
    {
        var query = withNoTracking ? dbContext.Set<T>().AsNoTracking() : dbContext.Set<T>();
        return await query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await dbContext.Set<T>().FindAsync(id);
    }

    public async Task<T?> GetByCriteriaAsync(ISpecifications<T> specification)
    {
        var query = ApplySpecification(specification);
        return await query.FirstOrDefaultAsync();
    }

    public async Task AddAsync(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        dbContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        dbContext.Set<T>().Remove(entity);
    }

    public async Task<IEnumerable<T>> GetAllBySpecificationAsync(ISpecifications<T> specification)
    {
        var query = ApplySpecification(specification);
        return await query.ToListAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecifications<T> specification)
    {
        return SpecificationQueryEvaluator<T>.GetQuery(dbContext.Set<T>(), specification);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await dbContext.Set<T>().AddRangeAsync(entities);
    }
}
