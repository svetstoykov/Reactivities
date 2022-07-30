using System.Threading.Tasks;
using Application.Common.DataServices;
using Domain.Profiles;

namespace Application.Profiles.DataServices
{
    public interface IProfileDataService : IEntityDataService<Profile>
    {
        Task<Profile> GetByIdAsync(int id);
    }
}
