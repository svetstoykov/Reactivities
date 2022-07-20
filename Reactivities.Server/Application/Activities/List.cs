using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Application.Activities.Models.Output;
using Application.Common;
using AutoMapper;
using MediatR;
using Models.Common;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<Result<IEnumerable<ActivityOutputModel>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<IEnumerable<ActivityOutputModel>>>
        {
            private readonly IActivitiesDataService _activitiesDataService;
            private readonly IMapper _mapper;

            public Handler(IActivitiesDataService activitiesDataService, IMapper mapper)
            {
                _activitiesDataService = activitiesDataService;
                _mapper = mapper;
            }

            public async Task<Result<IEnumerable<ActivityOutputModel>>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var activities = await this._activitiesDataService.GetActivitiesAsync();

                var outputModels = this._mapper.Map<IEnumerable<ActivityOutputModel>>(activities);

                return Result<IEnumerable<ActivityOutputModel>>.Success(outputModels);
            }
        }
    }
}