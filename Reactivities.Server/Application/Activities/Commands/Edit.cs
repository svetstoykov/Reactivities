using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Application.Activities.Models.Input;
using AutoMapper;
using MediatR;
using Models.Common;
using Models.ErrorHandling.Helpers;

namespace Application.Activities.Commands
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Command(EditActivityInputModel dto)
            {
                this.Dto = dto;
            }

            public EditActivityInputModel Dto { get; init; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IActivitiesDataService _activitiesDataService;
            private readonly IMapper _mapper;

            public Handler(IActivitiesDataService activitiesDataService, IMapper mapper)
            {
                this._activitiesDataService = activitiesDataService;
                this._mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var domainDto = await this._activitiesDataService
                    .GetByIdAsync(request.Dto.Id);
                
                this._mapper.Map(request.Dto, domainDto);

                this._activitiesDataService.Update(domainDto);

                await this._activitiesDataService.SaveChangesAsync(
                    cancellationToken, ActivitiesErrorMessages.EditError);

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
