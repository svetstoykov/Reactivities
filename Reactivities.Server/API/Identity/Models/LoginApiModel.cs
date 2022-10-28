using API.Common.Models;

namespace API.Identity.Models;

public class LoginApiModel : BaseApiModel
{
    public string Email { get; set; }

    public string Password { get; set; }
}