using System.Threading;
using System.Threading.Tasks;
using Application.Common.Identity.DataServices;
using Application.Profiles.DataServices;
using MediatR;
using Models.Common;

namespace Application.Profiles.Commands
{
    public class UpdateDetails
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Command(string displayName, string bio, string email, string userName)
            {
                DisplayName = displayName;
                Bio = bio;
                Email = email;
                UserName = userName;
            }
            
            public string DisplayName { get; }
            public string Bio { get; }
            public string Email { get; }
            public string UserName { get; }
        }
        
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IProfilesDataService _profilesDataService;
            private readonly IUserDataService _userDataService;

            public Handler(IProfilesDataService profilesDataService, IUserDataService userDataService)
            {
                this._profilesDataService = profilesDataService;
                this._userDataService = userDataService;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var profile = await this._profilesDataService
                    .GetByUsernameAsync(request.UserName);

                profile.UpdatePersonalInfo(request.Bio, request.DisplayName);
                
                if (profile.Email != request.Email)
                {
                    await _userDataService.ChangeEmailAddress(profile.Email, request.Email);

                    profile.UpdateEmailAddress(request.Email);
                }

                await this._profilesDataService.SaveChangesAsync(cancellationToken);
                
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}