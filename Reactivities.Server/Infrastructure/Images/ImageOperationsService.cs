using Application.Common.Images;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Infrastructure.Images.Models;
using Microsoft.Extensions.Options;
using Models.ErrorHandling;
using ApplicationModels = Application.Common.Images.Models;

namespace Infrastructure.Images
{
    public class ImageOperationsService : IImageOperationsService
    {
        private readonly Cloudinary _cloudinary;

        public ImageOperationsService(IOptionsMonitor<CloudinarySettings> cloudinarySettings)
        {
            var account = new Account(
                cloudinarySettings.CurrentValue.CloudName, 
                cloudinarySettings.CurrentValue.ApiKey,
                cloudinarySettings.CurrentValue.ApiSecret);

            this._cloudinary = new Cloudinary(account);
        }

        public async Task<ApplicationModels.ImageUploadResult> UploadImageAsync(byte[] fileByteArray, string fileName)
        {
            if (fileByteArray.Length > 0)
            {
                var fileStream = new MemoryStream(fileByteArray);
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileName, fileStream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill"),
                };

                var uploadResult = await this._cloudinary.UploadAsync(uploadParams);
                if (uploadResult.Error != null)
                {
                    throw new AppException(uploadResult.Error.Message);
                }

                return new ApplicationModels.ImageUploadResult(
                    uploadResult.PublicId, uploadResult.SecureUrl.ToString());
            }

            throw new AppException("Invalid uploaded image.");
        }

        public async Task DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var deleteResult = await this._cloudinary.DestroyAsync(deleteParams);

            if(deleteResult.Error != null)
                throw new AppException(deleteResult.Error.Message);
        }
    }
}
