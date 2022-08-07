using System.Threading;
using System.Threading.Tasks;
using Application.Profiles.DataServices;
using Application.Profiles.ErrorHandling;
using Application.Profiles.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Domain.Profiles;

namespace Application.Profiles.Commands
{
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
            private readonly IProfileFollowingsDataService _profileFollowingsDataService;
            private readonly IProfileAccessor _profileAccessor;

            public Handler(IProfilesDataService profilesDataService, IProfileFollowingsDataService profileFollowingsDataService, IProfileAccessor profileAccessor)
            {
                this._profilesDataService = profilesDataService;
                this._profileFollowingsDataService = profileFollowingsDataService;
                this._profileAccessor = profileAccessor;
            }

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                var loggedInUsername = this._profileAccessor.GetLoggedInUsername();
                if (loggedInUsername == request.UserToFollow)
                {
                    return Result<bool>.Failure(ProfileErrorMessages.CannotFollowYourself);
                }
                
                var observer = await this._profilesDataService.GetByUsernameAsync(loggedInUsername);

                var target = await this._profilesDataService.GetByUsernameAsync(request.UserToFollow);

                var following = await this._profileFollowingsDataService
                    .GetAsQueryable()
                    .FirstOrDefaultAsync(f => f.ObserverId == observer.Id && f.TargetId == target.Id, cancellationToken);

                if (following == null)
                {
                    this._profileFollowingsDataService.Create(ProfileFollowing.New(observer, target));
                }
                else
                {
                    this._profileFollowingsDataService.Remove(following);
                }

                await this._profilesDataService.SaveChangesAsync(cancellationToken);

                var isNowFollowed = following == null;
                
                return Result<bool>.Success(isNowFollowed);
            }
        }
    }
}