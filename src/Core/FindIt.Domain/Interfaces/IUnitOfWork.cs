using FindIt.Domain.Common;

namespace FindIt.Domain.Interfaces;
public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    IGenericRepository<T> Repository<T>() where T : class;
    Task<int> CompleteAsync();
}