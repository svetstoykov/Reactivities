using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Application.Activities.Models.Input;
using AutoMapper;
using Domain.Activities;
using MediatR;
using Models.Common;
using Models.ErrorHandling.Helpers;

namespace Application.Activities.Commands
{
    public class Create
    {
        public class Command : IRequest<Result<int>>
        {
            public Command(CreateActivityInputModel dto)
            {
                this.Dto = dto;
            }

            public CreateActivityInputModel Dto { get; init; }
        }

        public class Handler : IRequestHandler<Command, Result<int>>
        {
            private readonly IActivitiesDataService _activitiesDataService;
            private readonly IMapper _mapper;

            public Handler(IActivitiesDataService activitiesDataService, IMapper mapper)
            {
                this._activitiesDataService = activitiesDataService;
                this._mapper = mapper;
            }

            public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
            {
                var domainDto = this._mapper.Map<Activity>(request.Dto);

                this._activitiesDataService.Create(domainDto);

                await this._activitiesDataService
                    .SaveChangesAsync(cancellationToken, ActivitiesErrorMessages.CreateError);

                return Result<int>.Success(domainDto.Id);
            }
        }
    }
}