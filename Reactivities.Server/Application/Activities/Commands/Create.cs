using System.Threading;
using System.Threading.Tasks;
using Application.Activities.ErrorHandling;
using Application.Activities.Interfaces;
using Application.Activities.Interfaces.DataServices;
using Application.Activities.Models.Input;
using Domain.Activities;
using MediatR;
using Models.Common;

namespace Application.Activities.Commands;

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

        public Handler(IActivitiesDataService activitiesDataService)
        {
            this._activitiesDataService = activitiesDataService;
        }

        public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
        {
            var domainDto = Activity.New(
                request.Dto.Title, 
                request.Dto.Date, 
                request.Dto.Description, 
                request.Dto.City, 
                request.Dto.Venue, 
                (int)request.Dto.CategoryType, 
                request.Dto.HostId);

            this._activitiesDataService.Create(domainDto);

            await this._activitiesDataService
                .SaveChangesAsync(cancellationToken, ActivitiesErrorMessages.CreateError);

            return Result<int>.Success(domainDto.Id);
        }
    }
}