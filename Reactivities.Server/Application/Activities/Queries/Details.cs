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
            private readonly IProfileAccessor _profileAccessor;
            private readonly IMapper _mapper;

            public Handler(IActivitiesDataService activitiesDataService, IProfileAccessor profileAccessor,IMapper mapper)
            {
                this._activitiesDataService = activitiesDataService;
                this._profileAccessor = profileAccessor;
                this._mapper = mapper;
            }
    
            public async Task<Result<ActivityOutputModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await this._activitiesDataService
                    .GetAsQueryable()
                    .ProjectTo<ActivityOutputModel>(this._mapper.ConfigurationProvider,
                        new {currentProfile = this._profileAccessor.GetLoggedInUsername()})
                    .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

                return Result<ActivityOutputModel>.Success(activity);
            }
        }
    }
}