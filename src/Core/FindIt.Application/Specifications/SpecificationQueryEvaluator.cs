using FindIt.Domain.Common;
using FindIt.Domain.Interfaces;

namespace FindIt.Domain.Specifications;

/// <summary>
/// A static class that evaluates specifications against a queryable collection of entities.
/// </summary>
/// <typeparam name="T">The type of entities that the specifications apply to. Must be a reference type.</typeparam>
public static class SpecificationQueryEvaluator<T> where T : class
{
    /// <summary>
    /// Evaluates the given specifications against the input query and returns the resulting IQueryable.
    /// </summary>
    /// <param name="inputQuery">The input IQueryable representing the initial set of entities.</param>
    /// <param name="specifications">The specifications containing filtering, ordering, and pagination criteria.</param>
    /// <returns>An IQueryable of type <typeparamref name="T"/> that represents the filtered, ordered, and paginated result set.</returns>
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> specifications)
    {
        var query = inputQuery;

        // Apply where criteria if provided
        if (specifications.WhereCriteria is not null)
            query = query.Where(specifications.WhereCriteria);

        // Include specified navigation properties
        if (specifications.IncludesCriteria is not null && specifications.IncludesCriteria.Any())
        {
            foreach (var include in specifications.IncludesCriteria)
            {
                query = include(query);
            }
        }

        // Apply ordering criteria
        if (specifications.OrderBy is not null)
            query = query.OrderBy(specifications.OrderBy);
        else if (specifications.OrderByDesc is not null)
            query = query.OrderBy(specifications.OrderByDesc);

      
        // Apply pagination if enabled
        if (specifications.IsPaginationEnabled)
            query = query.Skip(specifications.Skip).Take(specifications.Take);

        return query;
    }
}