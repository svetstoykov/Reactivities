using System;
using System.Collections.Generic;

namespace Application.Common.Models
{
    public class PaginatedResult<TData>
    {
        public PaginatedResult(ICollection<TData> data, Pagination pagination)
        {
            this.Data = data;
            this.Pagination = pagination;
        }

        public ICollection<TData> Data { get; }

        public Pagination Pagination { get; }
    }

    public class Pagination
    {
        public Pagination(int pageSize, int pageNumber, int totalCount)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalCount = totalCount;
            TotalPages = (int) Math.Ceiling(totalCount / (double)pageSize);
        }

        public int PageSize { get; }

        public int PageNumber { get; }

        public int TotalCount { get; }

        public int TotalPages { get; }
    }
}

