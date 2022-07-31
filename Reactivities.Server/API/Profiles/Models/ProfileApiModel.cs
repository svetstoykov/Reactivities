using API.Common;

namespace API.Profiles.Models;

public class ProfileApiModel : BaseApiModel
{
    public string Email { get; set; }

    public string Bio { get; set; }

    public string DisplayName { get; set; }

    public string Username { get; set; }

    public string ProfilePictureUrl { get; set; }
}