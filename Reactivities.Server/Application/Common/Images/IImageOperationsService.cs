using System.Threading.Tasks;
using Application.Common.Images.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Images
{
    public interface IImageOperationsService
    {
        Task<ImageUploadResult> UploadImage(IFormFile file);

        Task DeleteImage(string publicId);
    }
}
