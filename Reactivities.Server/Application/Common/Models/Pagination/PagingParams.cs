using Application.Common.Models.Base;
using Reactivities.Common.ErrorHandling.Models;

namespace Application.Common.Models.Pagination;

public class PagingParams : BaseAppModel
{
    private int _pageSize;
    private int _pageNumber;
    private const int MaxPageSize = 50;

    private const int DefaultPageNumber = 1;
    private const int DefaultPageSize = 5;

    public int? PageSize
    {
        get => this._pageSize;
        set
        {
            if (this._pageSize > MaxPageSize)
            {
                throw new AppException("Maximum page size is 50");
            }

            this._pageSize = value ?? DefaultPageSize;
        }
    }

    public int? PageNumber
    {
        get => this._pageNumber;
        set => this._pageNumber = value ?? DefaultPageNumber;
    }
}