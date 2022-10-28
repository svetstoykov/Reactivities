using System.Threading;
using System.Threading.Tasks;
using Application.Pictures;
using Application.Pictures.Interfaces;
using Application.Pictures.Interfaces.DataServices;
using Application.Profiles.Interfaces;
using Application.Profiles.Interfaces.DataServices;
using MediatR;
using Models.Common;

namespace Application.Profiles.Commands;

public class DeleteProfilePicture
{
    public class Command : IRequest<Result<Unit>>
    {
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IPictureFileOperationsService _pictureFileOperationsService;
        private readonly IProfilesDataService _profilesDataService;
        private readonly IPicturesDataService _picturesDataService;
        private readonly IProfileAccessor _profileAccessor;

        public Handler(
            IPictureFileOperationsService pictureFileOperationsService, 
            IProfilesDataService profilesDataService, 
            IPicturesDataService picturesDataService, 
            IProfileAccessor profileAccessor)
        {
            this._pictureFileOperationsService = pictureFileOperationsService;
            this._profilesDataService = profilesDataService;
            this._picturesDataService = picturesDataService;
            this._profileAccessor = profileAccessor;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var profile = await this._profilesDataService
                .GetByUsernameAsync(this._profileAccessor.GetLoggedInUsername());

            await this._pictureFileOperationsService.DeletePictureAsync(profile.Picture.PublicId);

            this._picturesDataService.Remove(profile.Picture);

            await this._picturesDataService.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}