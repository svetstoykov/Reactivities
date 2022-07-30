using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Domain.Common.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models.Common;
using Models.ErrorHandling.Helpers;

namespace Application.Activities.Commands;

public class UpdateStatus
{
    public class Command : IRequest<Result<Unit>>
    {
        public Command(int activityId)
        {
            ActivityId = activityId;
        }

        public int ActivityId { get; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IActivitiesDataService _activitiesDataService;
        private readonly UserManager<User> _userManager;

        public Handler(IActivitiesDataService activitiesDataService, UserManager<User> userManager)
        {
            _activitiesDataService = activitiesDataService;
            _userManager = userManager;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await this._activitiesDataService
                .GetByIdAsync(request.ActivityId);

            activity.IsCancelled = !activity.IsCancelled;

            await _activitiesDataService.SaveChangesAsync(cancellationToken);
            
            return Result<Unit>.Success(Unit.Value);
        }
    }
}