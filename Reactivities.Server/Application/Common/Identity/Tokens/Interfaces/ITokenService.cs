using Domain.Common.Identity;

namespace Application.Common.Identity.Tokens.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
