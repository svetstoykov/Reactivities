using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Profiles.ErrorHandling;
using Application.Profiles.Interfaces;
using Application.Profiles.Interfaces.DataServices;
using MediatR;
using Models.Common;
using Domain.Profiles;

namespace Application.Profiles.Commands;

public class FollowToggle
{
    public class Command : IRequest<Result<bool>>
    {
        public Command(string userToFollow)
        {
            this.UserToFollow = userToFollow;
        }
            
        public string UserToFollow { get; }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly IProfilesDataService _profilesDataService;
        private readonly IFollowingsDataService _followingsDataService;
        private readonly IProfileAccessor _profileAccessor;

        public Handler(IProfilesDataService profilesDataService, IFollowingsDataService followingsDataService, IProfileAccessor profileAccessor)
        {
            this._profilesDataService = profilesDataService;
            this._followingsDataService = followingsDataService;
            this._profileAccessor = profileAccessor;
        }

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var loggedInUsername = this._profileAccessor.GetLoggedInUsername();
            if (loggedInUsername == request.UserToFollow)
            {
                return Result<bool>.Failure(ProfileErrorMessages.CannotFollowYourself);
            }

            var observer = await this._profilesDataService.GetProfileWithFollowings(
                loggedInUsername, cancellationToken);

            if (observer == null)
            {
                return Result<bool>.Failure(string.Format(
                    ProfileErrorMessages.ProfileDoesNotExist, loggedInUsername));
            }

            var following = observer.Followings
                .FirstOrDefault(p => p.Target.UserName == request.UserToFollow);

            if (following == null)
            {
                var target = await this._profilesDataService.GetByUsernameAsync(request.UserToFollow);

                observer.AddFollowing(ProfileFollowing.New(observer, target));
            }
            else
            {
                observer.RemoveFollowing(following);
            }

            await this._profilesDataService.SaveChangesAsync(cancellationToken);

            var isTargetUserNowFollowed = following == null;
                
            return Result<bool>.Success(isTargetUserNowFollowed);
        }
    }
}