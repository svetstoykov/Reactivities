using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.Interfaces.DataServices;
using Application.Activities.Models.Enums;
using Application.Activities.Models.Input;
using Application.Activities.Models.Output;
using Application.Common.Models.Pagination;
using Application.Profiles.Interfaces;
using Domain.Activities;
using MediatR;
using Reactivities.Common.Result.Models;

namespace Application.Activities.Queries;

public class List
{
    public class Query : IRequest<Result<PaginatedResult<ActivityOutputModel>>>
    {
        public Query(ActivityListInputModel dto)
        {
            this.Dto = dto;
        }

        public ActivityListInputModel Dto { get; }
    }

    public class Handler : IRequestHandler<Query, Result<PaginatedResult<ActivityOutputModel>>>
    {
        private readonly IActivitiesDataService _activitiesDataService;
        private readonly IProfileAccessor _profileAccessor;

        public Handler(IActivitiesDataService activitiesDataService, IProfileAccessor profileAccessor)
        {
            this._activitiesDataService = activitiesDataService;
            this._profileAccessor = profileAccessor;
        }

        public async Task<Result<PaginatedResult<ActivityOutputModel>>> Handle(
            Query request, CancellationToken cancellationToken)
        {
            var loggedInUsername = this._profileAccessor.GetLoggedInUsername();

            var activitiesQueryable = this._activitiesDataService
                .GetAsQueryable()
                .Where(a => !request.Dto.StartDate.HasValue || a.Date.Date >= request.Dto.StartDate.Value.Date);

            activitiesQueryable = ApplyAttendanceFilter(
                request.Dto.Attending, activitiesQueryable, loggedInUsername);

            var pageIndex = request.Dto.PageNumber - 1;
                
            var activitiesList = await this._activitiesDataService.GetPaginatedActivitiesAsync(
                activitiesQueryable, 
                request.Dto.PageSize, 
                pageIndex, 
                loggedInUsername,
                cancellationToken);

            return Result<PaginatedResult<ActivityOutputModel>>.Success(activitiesList);
        }

        private static IQueryable<Activity> ApplyAttendanceFilter(
            ActivityAttendingFilterType attendanceFilter, IQueryable<Activity> activities, string loggedInUsername)
            => attendanceFilter switch
            {
                ActivityAttendingFilterType.AllActivities => activities,
                ActivityAttendingFilterType.ImHosting => activities
                    .Where(a => a.Host.UserName == loggedInUsername),
                ActivityAttendingFilterType.ImGoing => activities
                    .Where(a => a.Attendees.Any(attendee => attendee.UserName == loggedInUsername)),
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}