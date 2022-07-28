using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Application.Activities.Models.Output;
using AutoMapper;
using MediatR;
using Models.Common;

namespace Application.Activities
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
                var domainDto = await this._activitiesDataService
                    .GetByIdAsync(request.Id);

                var outputModel = this._mapper.Map<ActivityOutputModel>(domainDto);

                return Result<ActivityOutputModel>.Success(outputModel);
            }
        }
    }
}