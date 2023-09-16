using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.Models.Output;
using Application.Common.Models.Pagination;
using Application.Profiles.Models.Enums;
using Application.Profiles.Models.Output;
using Domain.Activities;
using Reactivities.Common.DataServices.Abstractions.Interfaces;

namespace Application.Activities.Interfaces.DataServices;

public interface IActivitiesDataService : IEntityDataService<Activity>
{
    Task<PaginatedResult<ActivityOutputModel>> GetPaginatedActivitiesAsync(
        IQueryable<Activity> activitiesQueryable, 
        int pageSize, 
        int pageIndex, 
        string loggedInUsername, 
        CancellationToken cancellationToken = default);

    Task<ICollection<Category>> GetCategoriesAsync(
        Func<Category, bool> predicate = null,
        CancellationToken cancellationToken = default);

    Task<ActivityOutputModel> GetActivityOutputModel(
        int activityId, 
        CancellationToken cancellationToken = default);

    Task<ICollection<ProfileActivityOutputModel>> GetProfileFilteredActivitiesAsync(
        string username,
        ProfileActivitiesFilterType filter, 
        CancellationToken cancellationToken = default);

    Task<Activity> GetByIdAsync(int id, bool throwExceptionIfNull = true);
}