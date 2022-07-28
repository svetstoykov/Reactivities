using API.Common.Identity.Models.Base;

namespace API.Common.Identity.Models;

public class ProfileApiModel : BaseUserApiModel
{
    public string Bio { get; set; }
}