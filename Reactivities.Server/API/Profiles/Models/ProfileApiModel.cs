using API.Common.Identity.Models.Base;

namespace API.Profiles.Models;

public class ProfileApiModel : BaseUserApiModel
{
    public string Bio { get; set; }
}