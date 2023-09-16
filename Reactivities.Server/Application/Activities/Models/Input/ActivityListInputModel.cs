using System;
using Application.Activities.Models.Enums;
using Application.Common.Models.Pagination;

namespace Application.Activities.Models.Input;

public class ActivityListInputModel : PagingParams
{
    public ActivityAttendingFilterType Attending { get; set; }

    public DateTime? StartDate { get; set; } = null;
}