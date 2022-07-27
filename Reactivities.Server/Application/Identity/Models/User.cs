using Microsoft.AspNetCore.Identity;

namespace Application.Identity.Models
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }

        public string Bio { get; set; }
    }
}
