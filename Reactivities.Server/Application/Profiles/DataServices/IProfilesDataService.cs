using System.Threading;
using System.Threading.Tasks;
using Application.Common.DataServices;
using Application.Profiles.Models;
using Domain.Profiles;

namespace Application.Profiles.DataServices
{
    public interface IProfilesDataService : IEntityDataService<Profile>
    {
        Task<Profile> GetByUsernameAsync(
            string username, bool throwExceptionIfNull = true);

        Task<ProfileOutputModel> GetProfileOutputModel(
            string username, CancellationToken cancellationToken = default);

        Task<Profile> GetProfileWithFollowings(
            string username, CancellationToken cancellationToken);
    }
}
