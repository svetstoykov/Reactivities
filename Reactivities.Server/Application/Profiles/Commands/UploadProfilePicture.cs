using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Pictures;
using Application.Profiles.DataServices;
using MediatR;
using Models.Common;

namespace Application.Profiles.Commands
{
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
         
            public string Username { get; }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly IPictureOperationsService _pictureOperationsService;
            private readonly IProfilesDataService _profilesDataService;
            private readonly IPicturesDataService _picturesDataService;

            public Handler(IPictureOperationsService pictureOperationsService, IProfilesDataService profilesDataService, IPicturesDataService picturesDataService)
            {
                this._pictureOperationsService = pictureOperationsService;
                this._profilesDataService = profilesDataService;
                this._picturesDataService = picturesDataService;
            }

            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var profile = await this._profilesDataService
                    .GetByUsernameAsync(request.Username);
                if (profile.Picture != null)
                {
                    this._picturesDataService.Remove(profile.Picture);
                }

                var imageUpload = await this._pictureOperationsService
                    .UploadPictureAsync(request.FileByteArray, request.FileName);

                profile.AddPicture(imageUpload.PublicId, imageUpload.Url);

                await this._profilesDataService.SaveChangesAsync();

                return Result<string>.Success(imageUpload.Url);
            }
        }
    }
}
