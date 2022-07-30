using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Application.Profiles.DataServices;
using MediatR;
using Models.Common;
using Models.ErrorHandling.Helpers;

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
        private readonly IProfilesDataService _profilesDataService;

        public Handler(IActivitiesDataService activitiesDataService, IProfilesDataService profilesDataService)
        {
            this._activitiesDataService = activitiesDataService;
            this._profilesDataService = profilesDataService;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await this._activitiesDataService
                .GetByIdAsync(request.ActivityId);

            var profile = await this._profilesDataService
                .GetByIdAsync(request.UserToAttend);
            if (profile == null)
            {
                return Result<Unit>.NotFound(
                    IdentityErrorMessages.InvalidUser);
            }

            if (activity.HostId == profile.Id)
            {
                return Result<Unit>.Failure(
                    ActivitiesErrorMessages.HostCannotBeAddedAsAttendee);
            }

            if (!activity.Attendees.Remove(profile))
            {
                activity.Attendees.Add(profile);
            }

            await this._activitiesDataService.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}