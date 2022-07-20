using Microsoft.AspNetCore.Identity;

namespace API.Identity.Models
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }

        public string Bio { get; set; }
    }
}
