using FindIt.Domain.Common;
using FindIt.Domain.Interfaces;
using FindIt.Persistence.Context;
using System.Collections.Concurrent;

namespace FindIt.Persistence.Repositories.Classes;
public class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
{   
    private readonly ConcurrentDictionary<string, object> _repositories = new();
    public IGenericRepository<T> Repository<T>() where T : class
    {
        var type = typeof(T).Name;
        return (IGenericRepository<T>)_repositories.GetOrAdd(type, _ => new GenericRepository<T>(dbContext));
    }
    public async Task<int> CompleteAsync()=> await dbContext.SaveChangesAsync();
    public void Dispose()=> dbContext.Dispose();
    public async ValueTask DisposeAsync()=> await dbContext.DisposeAsync();
}