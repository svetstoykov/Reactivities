namespace Application.Pictures.Models.Output;

public class ImageUploadOutputModel
{
    public ImageUploadOutputModel(string publicId, string url)
    {
        this.PublicId = publicId;
        this.Url = url;
    }

    public string PublicId { get; }
        
    public string Url { get; }
}