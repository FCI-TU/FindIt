using FindIt.Persistence.Interfaces;
using System.Linq.Expressions;
using FindIt.Domain.Common;

namespace FindIt.Persistence.Specifications;

/// <summary>
/// Represents the base class for specifications that can be applied to a queryable collection of entities.
/// </summary>
/// <typeparam name="T">The type of entities that the specifications apply to. Must be a reference type.</typeparam>

public class BaseSpecifications<T> : ISpecifications<T> where T : Entity
{

    #region  Properties

    // Gets the criteria for filtering entities
    public Expression<Func<T, bool>>? WhereCriteria { get; private set; }

    // Gets the list of criteria for including related entities.
    public List<Expression<Func<T, object>>> IncludesCriteria { get; private set; } = [];

    // Gets the criteria for ordering entities in ascending order.
    public Expression<Func<T, object>>? OrderBy { get; private set; }

    // Gets the criteria for ordering entities in descending order.
    public Expression<Func<T, object>>? OrderByDesc { get; private set; }

    /// <summary>
    /// Get the pagination information,
    /// number of entities to skip for pagination,
    /// and number of entities to take for pagination
    /// </summary>
    public int Skip { get; private set; }
    public int Take { get; private set; }
    public bool IsPaginationEnabled { get; private set; }

    #endregion

    #region Methods

    public void AddWhere(Expression<Func<T, bool>> whereExpression)
    {
        WhereCriteria = whereExpression;
    }

    public void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    public void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDesc = orderByDescExpression;
    }

    public void AddInclude(Expression<Func<T, object>> include)
    {
        IncludesCriteria.Add(include);
    }

    public void AddThenInclude<TPreviousProperty>(Expression<Func<TPreviousProperty, object>> thenInclude)
    {
        if (thenInclude is Expression<Func<T, object>> convertedThenInclude)
            IncludesCriteria.Add(convertedThenInclude);
        else
            throw new InvalidOperationException("Invalid type conversion for ThenInclude");
    }


    public void ApplyPagination(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPaginationEnabled = true;
    }

    public void ClearPagination()
    {
        Skip = 0;
        Take = 0;
        IsPaginationEnabled = false;
    }

    public void ClearIncludes()
    {
        IncludesCriteria.Clear();
    }

    #endregion

}