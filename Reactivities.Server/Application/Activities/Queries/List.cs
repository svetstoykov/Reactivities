using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Application.Activities.Models.Output;
using Application.Profiles.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.Common;

namespace Application.Activities.Queries
{
    public class List
    {
        public class Query : IRequest<Result<IEnumerable<ActivityOutputModel>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<IEnumerable<ActivityOutputModel>>>
        {
            private readonly IActivitiesDataService _activitiesDataService;
            private readonly IProfileAccessor _profileAccessor;
            private readonly IMapper _mapper;

            public Handler(IActivitiesDataService activitiesDataService, IProfileAccessor profileAccessor, IMapper mapper)
            {
                this._activitiesDataService = activitiesDataService;
                this._profileAccessor = profileAccessor;
                this._mapper = mapper;
            }

            public async Task<Result<IEnumerable<ActivityOutputModel>>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var activities = await this._activitiesDataService
                    .GetAsQueryable()
                    .ProjectTo<ActivityOutputModel>(this._mapper.ConfigurationProvider,
                        new {currentProfile = this._profileAccessor.GetLoggedInUsername()})
                    .ToListAsync(cancellationToken);

                return Result<IEnumerable<ActivityOutputModel>>.Success(activities);
            }
        }
    }
}