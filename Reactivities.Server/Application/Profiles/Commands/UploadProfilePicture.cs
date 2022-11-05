using System.Threading;
using System.Threading.Tasks;
using Application.Pictures.Interfaces;
using Application.Pictures.Interfaces.DataServices;
using Application.Profiles.Interfaces;
using Application.Profiles.Interfaces.DataServices;
using MediatR;
using Reactivities.Common.Result.Models;

namespace Application.Profiles.Commands;

public class UploadProfilePicture
{
    public class Command : IRequest<Result<string>>
    {
        public Command(byte[] fileByteArray, string fileName)
        {
            this.FileByteArray = fileByteArray;
            this.FileName = fileName;
        }

        public byte[] FileByteArray { get; }

        public string FileName { get; }
    }

    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly IProfilesDataService _profilesDataService;
        private readonly IProfileAccessor _profileAccessor;
        private readonly IPictureFileOperationsService _pictureFileOperationsService;
        private readonly IPicturesDataService _picturesDataService;

        public Handler(
            IProfilesDataService profilesDataService,
            IProfileAccessor profileAccessor,
            IPictureFileOperationsService pictureFileOperationsService, 
            IPicturesDataService picturesDataService)
        {
            this._profilesDataService = profilesDataService;
            this._profileAccessor = profileAccessor;
            this._pictureFileOperationsService = pictureFileOperationsService;
            this._picturesDataService = picturesDataService;
        }

        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var profile = await this._profilesDataService
                .GetByUsernameAsync(this._profileAccessor.GetLoggedInUsername());
            if (profile.Picture != null)
            {
                this._picturesDataService.Remove(profile.Picture);
            }

            var imageUpload = await this._pictureFileOperationsService
                .UploadPictureAsync(request.FileByteArray, request.FileName);

            profile.AddPicture(imageUpload.PublicId, imageUpload.Url);

            await this._profilesDataService.SaveChangesAsync(cancellationToken);

            return Result<string>.Success(imageUpload.Url);
        }
    }
}