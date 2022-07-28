using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Application.Activities.Models.Output;
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
            private readonly IMapper _mapper;

            public Handler(IActivitiesDataService activitiesDataService,IMapper mapper)
            {
                this._activitiesDataService = activitiesDataService;
                this._mapper = mapper;
            }
    
            public async Task<Result<ActivityOutputModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await this._activitiesDataService
                    .GetActivitiesQueryable()
                    .ProjectTo<ActivityOutputModel>(this._mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

                return Result<ActivityOutputModel>.Success(activity);
            }
        }
    }
}