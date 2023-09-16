using Application.Common.Models.Base;
using Reactivities.Common.ErrorHandling.Models;

namespace Application.Common.Models.Pagination;

public class PagingParams : BaseAppModel
{
    private int _pageSize;
    private int _pageNumber;
    private const int MaxPageSize = 50;
    private const int MinValue = 1;

    public int PageSize
    {
        get => this._pageSize;
        set
        {
            this._pageSize = value switch
            {
                > MaxPageSize => throw new AppException("Maximum page size is 50"),
                < MinValue => throw new AppException("Page size must be greater than 0"),
                _ => value
            };
        }
    }

    public int PageNumber
    {
        get => this._pageNumber;
        set
        {
            if (value < MinValue)
            {
                throw new AppException("Page number must be greater than 0");
            }

            this._pageNumber = value;
        }
    }
}