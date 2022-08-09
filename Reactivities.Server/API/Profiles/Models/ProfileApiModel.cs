using API.Common.Models;

namespace API.Profiles.Models
{
    public class ProfileApiModel : BaseApiModel
    {
        public string Email { get; set; }

        public string Bio { get; set; }

        public string DisplayName { get; set; }

        public int FollowersCount { get; set; }
        
        public int FollowingsCount { get; set; }

        public bool Following { get; set; }
        public string Username { get; set; }

        public string PictureUrl { get; set; }
    }
}