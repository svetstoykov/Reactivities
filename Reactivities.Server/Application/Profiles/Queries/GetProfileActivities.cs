﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.Interfaces.DataServices;
using Application.Profiles.Models.Enums;
using Application.Profiles.Models.Output;
using MediatR;
using Reactivities.Common.Result.Models;

namespace Application.Profiles.Queries;

public class GetProfileActivities
{
    public class Query : IRequest<Result<IEnumerable<ProfileActivityOutputModel>>>
    {
        public Query(string username, ProfileActivitiesFilterType filter)
        {
            this.Username = username;
            this.Filter = filter;
        }

        public string Username { get; }

        public ProfileActivitiesFilterType Filter { get; }
    }

    public class Handler : IRequestHandler<Query, Result<IEnumerable<ProfileActivityOutputModel>>>
    {
        private readonly IActivitiesDataService _activitiesDataService;

        public Handler(IActivitiesDataService activitiesDataService)
        {
            this._activitiesDataService = activitiesDataService;
        }

        public async Task<Result<IEnumerable<ProfileActivityOutputModel>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var profileActivities = await this._activitiesDataService
                .GetProfileFilteredActivitiesAsync(request.Username, request.Filter, cancellationToken);

            return Result<IEnumerable<ProfileActivityOutputModel>>
                .Success(profileActivities);
        }
    }
}