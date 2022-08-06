using Application.Pictures;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Infrastructure.Pictures.Models;
using Microsoft.Extensions.Options;
using Models.ErrorHandling;
using ImageUploadResult = Application.Pictures.Models.ImageUploadResult;

namespace Infrastructure.Pictures
{
    public class PictureOperationsService : IPictureOperationsService
    {
        private readonly Cloudinary _cloudinary;

        public PictureOperationsService(IOptionsMonitor<CloudinarySettings> cloudinarySettings)
        {
            var account = new Account(
                cloudinarySettings.CurrentValue.CloudName, 
                cloudinarySettings.CurrentValue.ApiKey,
                cloudinarySettings.CurrentValue.ApiSecret);

            this._cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> UploadPictureAsync(byte[] fileByteArray, string fileName)
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

                return new ImageUploadResult(
                    uploadResult.PublicId, uploadResult.SecureUrl.ToString());
            }

            throw new AppException("Invalid uploaded image.");
        }

        public async Task DeletePictureAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var deleteResult = await this._cloudinary.DestroyAsync(deleteParams);

            if(deleteResult.Error != null)
                throw new AppException(deleteResult.Error.Message);
        }
    }
}
