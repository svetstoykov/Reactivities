using System.Threading;
using System.Threading.Tasks;
using Application.Profiles.DataServices;
using Application.Profiles.ErrorHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Domain.Profiles;

namespace Application.Profiles.Commands
{
    public class FollowToggle
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Command(string observerUsername, string targetUsername)
            {
                this.ObserverUsername = observerUsername;
                this.TargetUsername = targetUsername;
            }

            public string ObserverUsername { get; }
            
            public string TargetUsername { get; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IProfilesDataService _profilesDataService;
            private readonly IProfileFollowingsDataService _profileFollowingsDataService;

            public Handler(IProfilesDataService profilesDataService, IProfileFollowingsDataService profileFollowingsDataService)
            {
                this._profilesDataService = profilesDataService;
                this._profileFollowingsDataService = profileFollowingsDataService;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.ObserverUsername == request.TargetUsername)
                {
                    return Result<Unit>.Failure(ProfileErrorMessages.CannotFollowYourself);
                }
                
                var observer = await this._profilesDataService.GetByUsernameAsync(request.ObserverUsername);

                var target = await this._profilesDataService.GetByUsernameAsync(request.TargetUsername);

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

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}