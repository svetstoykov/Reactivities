using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.Models.Output;
using Application.Common.DataServices;
using Application.Common.Models.Pagination;
using Application.Profiles.Models;
using Application.Profiles.Models.Enums;
using Domain.Activities;

namespace Application.Activities.DataServices
{
    public interface IActivitiesDataService : IEntityDataService<Activity>
    {
        Task<PaginatedResult<ActivityOutputModel>> GetPaginatedActivitiesAsync(
            IQueryable<Activity> activitiesQueryable, int pageSize, int pageNumber, string loggedInUsername, CancellationToken cancellationToken = default);

        Task<ICollection<Category>> GetCategoriesAsync(
            Func<Category, bool> predicate = null, CancellationToken cancellationToken = default);

        Task<ActivityOutputModel> GetActivityOutputModel(
            int activityId, CancellationToken cancellationToken = default);


        Task<ICollection<ProfileActivityOutputModel>> GetProfileFilteredActivitiesAsync(
            string username, ProfileActivitiesFilterType filter, CancellationToken cancellationToken = default);

        Task<Activity> GetByIdAsync(
            int id, bool throwExceptionIfNull = true);
    }
}