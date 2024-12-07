using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace FindIt.Domain.Interfaces;
public interface ISpecifications<T> where T : class
{
    Expression<Func<T, bool>>? WhereCriteria { get; }
    List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> IncludesCriteria { get; set; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDesc { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPaginationEnabled { get; }
}