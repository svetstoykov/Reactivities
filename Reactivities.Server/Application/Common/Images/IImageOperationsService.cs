using System.Threading.Tasks;
using Application.Common.Images.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Images
{
    public interface IImageOperationsService
    {
        Task<ImageUploadResult> UploadImageAsync(byte[] fileByteArray, string fileName);

        Task DeleteImageAsync(string publicId);
    }
}
