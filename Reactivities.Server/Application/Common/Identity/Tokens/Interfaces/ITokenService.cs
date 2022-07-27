using Application.Common.Identity.Models;

namespace Application.Common.Identity.Tokens.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
