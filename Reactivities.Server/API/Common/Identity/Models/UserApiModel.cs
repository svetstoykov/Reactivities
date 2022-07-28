namespace API.Common.Identity.Models
{
    public class UserApiModel : BaseApiModel
    {
        public string DisplayName { get; set; }

        public string Token { get; set; }

        public string Username { get; set; }

        public string Image { get; set; }
    }
}
