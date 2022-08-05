using System.Threading.Tasks;
using Application.Common.Images.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Images
{
    public interface IPictureOperationsService
    {
        Task<ImageUploadResult> UploadPictureAsync(byte[] fileByteArray, string fileName);

        Task DeletePictureAsync(string publicId);
    }
}
