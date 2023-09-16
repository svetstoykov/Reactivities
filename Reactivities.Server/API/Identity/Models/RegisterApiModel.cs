using API.Common.Models;

namespace API.Identity.Models;

public class RegisterApiModel : BaseApiModel
{
    public string DisplayName { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }
}