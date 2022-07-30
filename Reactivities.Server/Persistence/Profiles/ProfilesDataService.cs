using System.Threading.Tasks;
using Application.Profiles.DataServices;
using Domain.Profiles;
using Persistence.Common.DataServices;

namespace Persistence.Profiles
{
    public class ProfilesDataService : EntityDataService<Profile>, IProfilesDataService
    {
        public ProfilesDataService(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Profile> GetByIdAsync(int id)
            => await this.DataSet.FindAsync(id);
    }
}
