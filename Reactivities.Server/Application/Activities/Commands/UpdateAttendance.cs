using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Application.Profiles.DataServices;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.ErrorHandling.Helpers;
using User = Application.Common.Identity.Models.User;

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
        private readonly IProfileDataService _profileDataService;

        public Handler(IActivitiesDataService activitiesDataService, IProfileDataService profileDataService)
        {
            this._activitiesDataService = activitiesDataService;
            this._profileDataService = profileDataService;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await this._activitiesDataService
                .GetByIdAsync(request.ActivityId);

            var user = await this._profileDataService
                .GetByIdAsync(request.UserToAttend);
            if (user == null)
            {
                return Result<Unit>.NotFound(
                    IdentityErrorMessages.InvalidUser);
            }

            if (activity.HostId == user.Id)
            {
                return Result<Unit>.Failure(
                    ActivitiesErrorMessages.HostCannotBeAddedAsAttendee);
            }

            if (!activity.Attendees.Remove(user))
            {
                activity.Attendees.Add(user);
            }

            await this._activitiesDataService.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}