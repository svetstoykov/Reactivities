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
        public Command(int activityId, string userId)
        {
            ActivityId = activityId;
            UserId = userId;
        }
        
        public int ActivityId { get; }
        public string UserId { get; }
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
            if (activity == null)
            {
                return Result<Unit>.NotFound(string.Format(
                    ActivitiesErrorMessages.DoesNotExist, request.ActivityId));
            }

            var user = await this._userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return Result<Unit>.NotFound(IdentityErrorMessages.InvalidUser);
            }

            if (activity.HostId != user.Id)
            {
                return Result<Unit>.Failure(ActivitiesErrorMessages.UpdateStatusNotFromHost);
            }

            activity.IsCancelled = !activity.IsCancelled;

            return await _activitiesDataService.SaveChangesAsync(cancellationToken) > 0
                ? Result<Unit>.Success(Unit.Value)
                : Result<Unit>.Failure();
        }
    }
}