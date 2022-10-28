using Domain.Profiles;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Models;

public class User : IdentityUser
{
    public int ProfileId { get; set; }

    public Profile Profile { get; set; }
}