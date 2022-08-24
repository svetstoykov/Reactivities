using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Application.Activities.Models.Output;
using Application.Profiles.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Models.Common;

namespace Application.Activities.Queries
{
    public class Details
    {
        public class Query : IRequest<Result<ActivityOutputModel>>
        {
            public Query(int id)
            {
                this.Id = id;
            }

            public int Id { get; init; }
        }

        public class Handler : IRequestHandler<Query, Result<ActivityOutputModel>>
        {
            private readonly IActivitiesDataService _activitiesDataService;

            public Handler(IActivitiesDataService activitiesDataService)
            {
                this._activitiesDataService = activitiesDataService;
            }
    
            public async Task<Result<ActivityOutputModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await this._activitiesDataService
                    .GetActivityOutputModel(request.Id, cancellationToken);

                return Result<ActivityOutputModel>.Success(activity);
            }
        }
    }
}