using System.Threading;
using System.Threading.Tasks;
using Application.Common.Images;
using Application.Profiles.DataServices;
using MediatR;
using Models.Common;

namespace Application.Profiles.Commands
{
    public class DeleteProfilePicture
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Command(string username)
            {
                this.Username = username;
            }

            public string Username { get; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IImageOperationsService _imageOperationsService;
            private readonly IProfilesDataService _profilesDataService;

            public Handler(IImageOperationsService imageOperationsService, IProfilesDataService profilesDataService)
            {
                this._imageOperationsService = imageOperationsService;
                this._profilesDataService = profilesDataService;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var profile = await this._profilesDataService
                    .GetByUsernameAsync(request.Username);

                await this._imageOperationsService.DeleteImageAsync(profile.Picture.Url);

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
