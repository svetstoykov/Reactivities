using Application.Activities.ErrorHandling;
using Application.Activities.Interfaces.DataServices;
using Application.Activities.Models.Output;
using Application.Common.Models.Pagination;
using Application.Profiles.Interfaces;
using Application.Profiles.Models.Enums;
using Application.Profiles.Models.Output;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Activities;
using Infrastructure.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Reactivities.Common.DataServices.Services;

namespace Infrastructure.Activities.Services.DataServices;

public class ActivitiesDataService : EntityDataService<DataContext, Activity>, IActivitiesDataService
{
    private readonly IMapper _mapper;
    private readonly IProfileAccessor _profileAccessor;

    public ActivitiesDataService(DataContext dataContext, IMapper mapper, IProfileAccessor profileAccessor)
        : base(dataContext)
    {
        this._mapper = mapper;
        this._profileAccessor = profileAccessor;
    }

    public async Task<PaginatedResult<ActivityOutputModel>> GetPaginatedActivitiesAsync(
        IQueryable<Activity> activitiesQueryable, 
        int pageSize, 
        int pageIndex, 
        string loggedInUsername, 
        CancellationToken cancellationToken = default)
        => await activitiesQueryable
            .ProjectTo<ActivityOutputModel>(this._mapper.ConfigurationProvider,
                new {currentProfile = loggedInUsername})
            .PaginateAsync(pageIndex, pageSize, cancellationToken);

    public async Task<ICollection<Category>> GetCategoriesAsync(Func<Category, bool> predicate = null,
        CancellationToken cancellationToken = default)
        => await this.DataContext.Categories
            .Where(category => predicate == null || predicate(category))
            .ToListAsync(cancellationToken);

    public async Task<ActivityOutputModel> GetActivityOutputModel(
        int activityId, CancellationToken cancellationToken = default)
        => await this.DataSet.ProjectTo<ActivityOutputModel>(this._mapper.ConfigurationProvider,
                new {currentProfile = this._profileAccessor.GetLoggedInUsername()})
            .FirstOrDefaultAsync(a => a.Id == activityId, cancellationToken);

    public async Task<ICollection<ProfileActivityOutputModel>> GetProfileFilteredActivitiesAsync(
        string username, ProfileActivitiesFilterType filter, CancellationToken cancellationToken = default)
    {
        var activities = filter switch
        {
            ProfileActivitiesFilterType.ImHosting => this.DataSet
                .Where(a => a.Host.UserName == username),
            ProfileActivitiesFilterType.PastEvents => this.DataSet
                .Where(a => a.Attendees.Any(at => at.UserName == username) && a.Date < DateTime.UtcNow),
            ProfileActivitiesFilterType.UpcomingEvents => this.DataSet
                .Where(a => a.Attendees.Any(at => at.UserName == username) && a.Date >= DateTime.UtcNow),
            _ => throw new ArgumentOutOfRangeException()
        };

        return await activities
            .OrderByDescending(a => a.Date)
            .ProjectTo<ProfileActivityOutputModel>(this._mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<Activity> GetByIdAsync(int id, bool throwExceptionIfNull = true)
    {
        var activity = await this.DataSet
            .Include(a => a.Attendees)
            .Include(a => a.Host)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (throwExceptionIfNull && activity == null)
        {
            throw new KeyNotFoundException(string.Format(
                ActivitiesErrorMessages.DoesNotExist, id));
        }

        return activity;
    }
}