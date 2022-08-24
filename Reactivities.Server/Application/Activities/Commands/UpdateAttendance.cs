using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Application.Activities.ErrorHandling;
using Application.Profiles.DataServices;
using Application.Profiles.Services;
using MediatR;
using Models.Common;

namespace Application.Activities.Commands
{
    public class UpdateAttendance
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Command(int activityId)
            {
                this.ActivityId = activityId;
            }

            public int ActivityId { get; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IActivitiesDataService _activitiesDataService;
            private readonly IProfilesDataService _profilesDataService;
            private readonly IProfileAccessor _profileAccessor;

            public Handler(IActivitiesDataService activitiesDataService, IProfilesDataService profilesDataService, IProfileAccessor profileAccessor)
            {
                this._activitiesDataService = activitiesDataService;
                this._profilesDataService = profilesDataService;
                this._profileAccessor = profileAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await this._activitiesDataService
                    .GetByIdAsync(request.ActivityId);

                var profileToAttend = await this._profilesDataService
                    .GetByUsernameAsync(this._profileAccessor.GetLoggedInUsername());

                if (activity.HostId == profileToAttend.Id)
                {
                    return Result<Unit>.Failure(
                        ActivitiesErrorMessages.HostCannotBeAddedAsAttendee);
                }

                if (!activity.RemoveAttendee(profileToAttend))
                {
                    activity.AddAttendee(profileToAttend);
                }

                await this._activitiesDataService.SaveChangesAsync(cancellationToken);

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}