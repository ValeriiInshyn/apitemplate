#region

using Microsoft.EntityFrameworkCore;
using Server.Contracts.Responses;
using Server.Contracts.SubTypes;

#endregion

namespace Server.Infrastructure.Repositories.Extensions;

public static class PaginationExtensions
{
    /// <summary>
    /// It takes a query, a skip count, and a take count, and returns a tuple containing the paginated query and the total
    /// count of items in the query
    /// </summary>
    /// <param name="query">The query to paginate</param>
    /// <param name="skipItems">The number of items to skip.</param>
    /// <param name="takeItems">The number of items to take from the query.</param>
    /// <returns>
    /// A tuple of an IQueryable and an int.
    /// </returns>
    public static ( IQueryable<TEntity> Collection, int TotalCount) PaginateWithTotalCount<TEntity>(
        this IQueryable<TEntity> query, int skipItems, int takeItems)
    {
        return (query.Paginate(skipItems, takeItems), query.Count());
    }

    /// <summary>
    /// It takes a query, skips a number of items, takes a number of items, and returns a tuple of the paginated query and
    /// the total count of the query
    /// </summary>
    /// <param name="query">The query to paginate</param>
    /// <param name="skipItems">The number of items to skip.</param>
    /// <param name="takeItems">The number of items to take from the query.</param>
    /// <returns>
    /// A tuple containing the paginated query and the total count of the query.
    /// </returns>
    public static async Task<( IQueryable<TEntity> Collection, int TotalCount)> PaginateWithTotalCountAsync<TEntity>(
        this IQueryable<TEntity> query, int skipItems, int takeItems)
    {
        return (query.Paginate(skipItems, takeItems), await query.CountAsync());
    }

    /// <summary>
    /// It takes a query, skips a number of items, takes a number of items, and returns a tuple of the items and the total
    /// count of the query
    /// </summary>
    /// <param name="query">The query to paginate</param>
    /// <param name="skipItems">The number of items to skip.</param>
    /// <param name="takeItems">The number of items to take from the query.</param>
    /// <returns>
    /// A tuple of a list of TEntity and an int.
    /// </returns>
    public static async Task<(List<TEntity> Collection, int TotalCount)> PaginateWithTotalCountAsListAsync<TEntity>(
        this IQueryable<TEntity> query, int skipItems, int takeItems)
    {
        return (await query.Paginate(skipItems, takeItems).ToListAsync(), await query.CountAsync());
    }

    /// <summary>
    /// > It takes a query, a pageData object, and a cancellation token, and returns a PagedResponse object with the data
    /// and the total count of the query
    /// </summary>
    /// <param name="query">The query to paginate</param>
    /// <param name="pageData">This is the object that contains the pagination data.</param>
    /// <param name="CancellationToken">This is a token that can be used to cancel the operation.</param>
    /// <returns>
    /// A PagedResponse<TEntity> object.
    /// </returns>
    public static async Task<PagedResponse<TEntity>> PaginateWithTotalCountAsListAsync<TEntity>(
        this IQueryable<TEntity> query, PageData? pageData, CancellationToken cancellationToken)
    {
        if (pageData is not null)
            return new PagedResponse<TEntity>(
                await query.Paginate(pageData.Offset, pageData.Limit).ToListAsync(cancellationToken),
                await query.CountAsync()
            );
        var data = await query.ToListAsync(cancellationToken);
        return new PagedResponse<TEntity>(
            data,
            data.Count
        );
    }

    /// <summary>
    /// It takes a query, skips a number of items, and then takes a number of items
    /// </summary>
    /// <param name="query">The query to paginate.</param>
    /// <param name="skipItems">The number of items to skip.</param>
    /// <param name="takeItems">The number of items to take from the query.</param>
    /// <returns>
    /// A queryable object that is the result of the Skip and Take methods being called on the queryable object passed in.
    /// </returns>
    public static IQueryable<TEntity> Paginate<TEntity>(
        this IQueryable<TEntity> query, int skipItems, int takeItems)
    {
        return query.Skip(skipItems).Take(takeItems);
    }
}