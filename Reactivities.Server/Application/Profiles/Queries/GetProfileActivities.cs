using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Application.Profiles.Models;
using Application.Profiles.Models.Enums;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Activities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.Common;

namespace Application.Profiles.Queries
{
    public class GetProfileActivities
    {
        public class Query : IRequest<Result<IEnumerable<ProfileActivityOutputModel>>>
        {
            public Query(string username, ProfileActivitiesFilterType filter)
            {
                Username = username;
                Filter = filter;
            }

            public string Username { get; }

            public ProfileActivitiesFilterType Filter { get; }
        }

        public class Handler : IRequestHandler<Query, Result<IEnumerable<ProfileActivityOutputModel>>>
        {
            private readonly IActivitiesDataService _activitiesDataService;
            private readonly IMapper _mapper;

            public Handler(IActivitiesDataService activitiesDataService, IMapper mapper)
            {
                _activitiesDataService = activitiesDataService;
                _mapper = mapper;
            }

            public async Task<Result<IEnumerable<ProfileActivityOutputModel>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activities = this._activitiesDataService
                    .GetAsQueryable();

                activities = FilterProfileActivities(request, activities);

                var activitiesList = await activities
                    .OrderByDescending(a => a.Date)
                    .ProjectTo<ProfileActivityOutputModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return Result<IEnumerable<ProfileActivityOutputModel>>
                    .Success(activitiesList);
            }

            private static IQueryable<Activity> FilterProfileActivities(Query request, IQueryable<Activity> activities)
                => request.Filter switch
                {
                    ProfileActivitiesFilterType.ImHosting => activities
                        .Where(a => a.Host.UserName == request.Username),
                    ProfileActivitiesFilterType.PastEvents => activities
                        .Where(a => a.Attendees.Any(at => at.UserName == request.Username) && a.Date < DateTime.UtcNow),
                    ProfileActivitiesFilterType.UpcomingEvents => activities
                        .Where(a => a.Attendees.Any(at => at.UserName == request.Username) && a.Date >= DateTime.UtcNow),
                    _ => throw new ArgumentOutOfRangeException()
                };
        }
    }
}
