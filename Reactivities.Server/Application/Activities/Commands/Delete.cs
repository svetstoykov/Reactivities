using System.Threading;
using System.Threading.Tasks;
using Application.Activities.ErrorHandling;
using Application.Activities.Interfaces.DataServices;
using MediatR;
using Reactivities.Common.Result.Models;

namespace Application.Activities.Commands;

public class Delete
{
    public class Command : IRequest<Result<Unit>>
    {
        public Command(int id)
        {
            this.Id = id;
        }

        public int Id { get; init; }
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
                .GetByIdAsync(request.Id);

            this._activitiesDataService.Remove(domainDto);

            await this._activitiesDataService.SaveChangesAsync(
                cancellationToken, ActivitiesErrorMessages.DeleteError);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}