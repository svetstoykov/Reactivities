using Microsoft.AspNetCore.Identity;

namespace Application.Common.Identity.Models
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }

        public string Bio { get; set; }
    }
}
