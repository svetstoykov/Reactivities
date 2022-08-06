namespace Application.Pictures.Models
{
    public class ImageUploadResult
    {
        public ImageUploadResult(string publicId, string url)
        {
            this.PublicId = publicId;
            this.Url = url;
        }

        public string PublicId { get; }
        
        public string Url { get; }
    }
}
