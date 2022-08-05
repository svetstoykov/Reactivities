using System.Threading;
using System.Threading.Tasks;
using Application.Common.Images;
using Application.Pictures;
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
            private readonly IPictureOperationsService _pictureOperationsService;
            private readonly IProfilesDataService _profilesDataService;
            private readonly IPicturesDataService _picturesDataService;

            public Handler(
                IPictureOperationsService pictureOperationsService, 
                IProfilesDataService profilesDataService, 
                IPicturesDataService picturesDataService)
            {
                this._pictureOperationsService = pictureOperationsService;
                this._profilesDataService = profilesDataService;
                this._picturesDataService = picturesDataService;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var profile = await this._profilesDataService
                    .GetByUsernameAsync(request.Username);

                await this._pictureOperationsService.DeletePictureAsync(profile.Picture.PublicId);

                this._picturesDataService.Remove(profile.Picture);

                await this._picturesDataService.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
