using System.Threading;
using System.Threading.Tasks;
using Application.Pictures;
using Application.Profiles.DataServices;
using Application.Profiles.Services;
using MediatR;
using Models.Common;

namespace Application.Profiles.Commands
{
    public class DeleteProfilePicture
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Command()
            {
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IPictureOperationsService _pictureOperationsService;
            private readonly IProfilesDataService _profilesDataService;
            private readonly IPicturesDataService _picturesDataService;
            private readonly IProfileAccessor _profileAccessor;

            public Handler(
                IPictureOperationsService pictureOperationsService, 
                IProfilesDataService profilesDataService, 
                IPicturesDataService picturesDataService, 
                IProfileAccessor profileAccessor)
            {
                this._pictureOperationsService = pictureOperationsService;
                this._profilesDataService = profilesDataService;
                this._picturesDataService = picturesDataService;
                this._profileAccessor = profileAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var profile = await this._profilesDataService
                    .GetByUsernameAsync(this._profileAccessor.GetLoggedInUsername());

                await this._pictureOperationsService.DeletePictureAsync(profile.Picture.PublicId);

                this._picturesDataService.Remove(profile.Picture);

                await this._picturesDataService.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
