using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Application.Activities.Models.Output;
using Application.Common.Models;
using Application.Profiles.Services;
using MediatR;
using Models.Common;

namespace Application.Activities.Queries
{
    public class List
    {
        public class Query : IRequest<Result<PaginatedResult<ActivityOutputModel>>>
        {
            public Query(int pageSize, int pageNumber)
            {
                PageSize = pageSize;
                PageNumber = pageNumber;
            }

            public int PageSize { get; }
            public int PageNumber { get; }
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
                var activities = await this._activitiesDataService.GetPaginatedActivitiesAsync( 
                    request.PageSize, request.PageNumber, this._profileAccessor.GetLoggedInUsername(), cancellationToken);

                return Result<PaginatedResult<ActivityOutputModel>>.Success(activities);
            }
        }
    }
}