using System.Threading;
using System.Threading.Tasks;
using Application.Activities.ErrorHandling;
using Application.Activities.Interfaces.DataServices;
using Application.Activities.Models.Input;
using MediatR;
using Reactivities.Common.Result.Models;

namespace Application.Activities.Commands;

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

        public Handler(IActivitiesDataService activitiesDataService)
        {
            this._activitiesDataService = activitiesDataService;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var domainDto = await this._activitiesDataService
                .GetByIdAsync(request.Dto.Id);
                
            domainDto.UpdateDetails(
                request.Dto.Title,
                request.Dto.Date,
                request.Dto.Description,
                request.Dto.City,
                request.Dto.Venue,
                (int)request.Dto.CategoryType);

            this._activitiesDataService.Update(domainDto);

            await this._activitiesDataService.SaveChangesAsync(
                cancellationToken, ActivitiesErrorMessages.EditError);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}