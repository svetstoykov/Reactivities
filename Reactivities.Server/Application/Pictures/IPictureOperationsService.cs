using System.Threading.Tasks;
using Application.Pictures.Models;

namespace Application.Pictures
{
    public interface IPictureOperationsService
    {
        Task<ImageUploadResult> UploadPictureAsync(byte[] fileByteArray, string fileName);

        Task DeletePictureAsync(string publicId);
    }
}
