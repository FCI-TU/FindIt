using FindIt.Domain.Common;
using FindIt.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace FindIt.Domain.Specifications;

/// <summary>
/// Represents the base class for specifications that can be applied to a queryable collection of entities.
/// </summary>
/// <typeparam name="T">The type of entities that the specifications apply to. Must be a reference type.</typeparam>

public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
{

    #region  Properties
    
    // Gets the criteria for filtering entities
    public Expression<Func<T, bool>>? WhereCriteria { get; set; }

    // Gets the list of criteria for including related entities.
    public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> IncludesCriteria { get; set; } = new List<Func<IQueryable<T>, IIncludableQueryable<T, object>>>();

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