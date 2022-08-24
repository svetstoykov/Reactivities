using System.Threading;
using System.Threading.Tasks;
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
                this.DisplayName = displayName;
                this.Bio = bio;
                this.Email = email;
                this.UserName = userName;
            }
            
            public string DisplayName { get; }
            public string Bio { get; }
            public string Email { get; }
            public string UserName { get; }
        }
        
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IProfilesDataService _profilesDataService;

            public Handler(IProfilesDataService profilesDataService)
            {
                this._profilesDataService = profilesDataService;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var profile = await this._profilesDataService
                    .GetByUsernameAsync(request.UserName);

                profile.UpdatePersonalInfo(request.Bio, request.DisplayName);
                
                await this._profilesDataService.SaveChangesAsync(cancellationToken);
                
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}