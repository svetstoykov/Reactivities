using System.Threading.Tasks;
using Application.Pictures.Models;
using Application.Pictures.Models.Output;

namespace Application.Pictures.Interfaces;

public interface IPictureFileOperationsService
{
    Task<ImageUploadOutputModel> UploadPictureAsync(byte[] fileByteArray, string fileName);

    Task DeletePictureAsync(string publicId);
}