using System.Threading;
using System.Threading.Tasks;
using Models.Common;

namespace Application.Identity.Interfaces;

public interface IUserIdentityService
{
    Task<Result<string>> LoginAndGenerateTokenAsync(
        string email, string password, CancellationToken cancellationToken = default);

    Task<Result<string>> RegisterAndGenerateTokenAsync(
        string username, string email, string password, string displayName, CancellationToken cancellationToken = default);
}