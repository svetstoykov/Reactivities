using System.Collections.Generic;

namespace Application.Common.Models.Pagination
{
    public class PaginatedResult<TData> : BaseAppModel
    {
        public PaginatedResult(ICollection<TData> data, Pagination pagination)
        {
            this.Data = data;
            this.Pagination = pagination;
        }

        public ICollection<TData> Data { get; }

        public Pagination Pagination { get; }
    }
}