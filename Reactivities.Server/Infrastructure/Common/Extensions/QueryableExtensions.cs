using Application.Common.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginatedResult<TData>> PaginateAsync<TData>(
        this IQueryable<TData> query,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await query.CountAsync(cancellationToken);

        var results = await query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<TData>(
            results, new Pagination(pageSize, pageIndex, totalCount));
    }
}