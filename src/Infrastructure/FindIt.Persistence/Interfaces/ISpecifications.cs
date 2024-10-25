using System.Linq.Expressions;
namespace FindIt.Persistence.Interfaces;

public interface ISpecifications<T> where T : class
{
    Expression<Func<T, bool>>? WhereCriteria { get; }
    List<Expression<Func<T, object>>> IncludesCriteria { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDesc { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPaginationEnabled { get; }
}