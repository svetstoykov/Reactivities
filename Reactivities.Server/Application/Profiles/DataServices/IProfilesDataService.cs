using System.Threading.Tasks;
using Application.Common.DataServices;
using Domain.Profiles;

namespace Application.Profiles.DataServices
{
    public interface IProfilesDataService : IEntityDataService<Profile>
    {
        Task<Profile> GetByIdAsync(int id);
    }
}
