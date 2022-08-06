using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Application.Activities.ErrorHandling;
using Application.Profiles.DataServices;
using MediatR;
using Models.Common;

namespace Application.Activities.Commands
{
    public class UpdateAttendance
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Command(int activityId, string usernameToAttend)
            {
                this.ActivityId = activityId;
                this.UsernameToAttend = usernameToAttend;
            }

            public int ActivityId { get; }
            public string UsernameToAttend { get; }
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
                    .GetByUsernameAsync(request.UsernameToAttend);

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
}