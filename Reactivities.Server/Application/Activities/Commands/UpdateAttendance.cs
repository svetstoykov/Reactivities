using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models.Common;
using Models.ErrorHandling.Helpers;
using User = Application.Common.Identity.Models.Base.User;

namespace Application.Activities.Commands;

public class UpdateAttendance
{
    public class Command : IRequest<Result<Unit>>
    {
        public Command(int activityId, int userToAttend)
        {
            this.ActivityId = activityId;
            this.UserToAttend = userToAttend;
        }

        public int ActivityId { get; }
        public int UserToAttend { get; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IActivitiesDataService _activitiesDataService;
        private readonly UserManager<User> _userManager;

        public Handler(IActivitiesDataService activitiesDataService, UserManager<User> userManager)
        {
            this._activitiesDataService = activitiesDataService;
            this._userManager = userManager;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await this._activitiesDataService
                .GetByIdAsync(request.ActivityId);
            
            var user = await this._userManager.FindByIdAsync(request.UserToAttend.ToString());
            if (user == null)
            {
                return Result<Unit>.NotFound(IdentityErrorMessages.InvalidUser);
            }

            if (activity.HostId == user.ProfileId)
            {
                return Result<Unit>.Failure(ActivitiesErrorMessages.HostCannotBeAddedAsAttendee);
            }

            if (!activity.Attendees.Remove(user.Profile))
            {
                activity.Attendees.Add(user.Profile);
            }

            await this._activitiesDataService.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}