using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Images;
using Application.Profiles.DataServices;
using MediatR;
using Models.Common;

namespace Application.Profiles.Commands
{
    public class UploadProfilePicture
    {
        public class Command : IRequest<Result<string>>
        {
            public Command(byte[] fileByteArray, string fileName, string username)
            {
                this.FileByteArray = fileByteArray;
                this.FileName = fileName;
                this.Username = username;
            }

            public byte[] FileByteArray { get; }

            public string FileName { get; }
         
            public string Username { get; }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly IImageOperationsService _imageOperationsService;
            private readonly IProfilesDataService _profilesDataService;

            public Handler(IImageOperationsService imageOperationsService, IProfilesDataService profilesDataService)
            {
                this._imageOperationsService = imageOperationsService;
                this._profilesDataService = profilesDataService;
            }

            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var profile = await this._profilesDataService
                    .GetByUsernameAsync(request.Username);

                var imageUpload = await this._imageOperationsService
                    .UploadImageAsync(request.FileByteArray, request.FileName);

                profile.AddPicture(imageUpload.PublicId, imageUpload.Url);

                await this._profilesDataService.SaveChangesAsync();

                return Result<string>.Success(imageUpload.Url);
            }
        }
    }
}
