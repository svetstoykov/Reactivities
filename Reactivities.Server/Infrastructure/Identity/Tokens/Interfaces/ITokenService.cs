using User = Infrastructure.Identity.Models.User;

namespace Infrastructure.Identity.Tokens.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
