using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using MediatR;
using Models.Common;
using Models.ErrorHandling.Helpers;

namespace Application.Activities
{
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
                var domainDto = await this._activitiesDataService.GetByIdAsync(request.Id);

                if (domainDto == null)
                {
                    return Result<Unit>.NotFound(
                        ActivitiesErrorMessages.DoesNotExist);
                }

                this._activitiesDataService.Remove(domainDto);

                var deleteResult = await this._activitiesDataService.SaveChangesAsync(cancellationToken) > 0;
                if (!deleteResult)
                {
                    return Result<Unit>.Failure(
                        ActivitiesErrorMessages.DeleteError);
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}