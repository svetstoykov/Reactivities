using System.Security.Claims;
using Application.Profiles.Services;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Profiles;

public class ProfileAccessor : IProfileAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public ProfileAccessor(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
    }

    public string GetLoggedInUsername()
        => this._httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
}