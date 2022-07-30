using User = Application.Common.Identity.Models.Base.User;

namespace Application.Common.Identity.Tokens.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
