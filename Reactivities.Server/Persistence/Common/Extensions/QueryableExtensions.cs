using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Common.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Common.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<TData>> PaginateAsync<TData>(
            this IQueryable<TData> query,
            int pageSize,
            int pageNumber,
            CancellationToken cancellationToken = default)
        {
            var totalCount = await query.CountAsync(cancellationToken);

            var results = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<TData>(
                results, new Pagination(pageSize, pageNumber, totalCount));
        }
    }
}
