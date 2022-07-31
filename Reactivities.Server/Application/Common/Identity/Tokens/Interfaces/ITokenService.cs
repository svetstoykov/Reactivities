using User = Application.Common.Identity.Models.User;

namespace Application.Common.Identity.Tokens.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
