using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Profiles.DataServices;
using Application.Profiles.ErrorHandling;
using Domain.Profiles;
using Microsoft.EntityFrameworkCore;
using Persistence.Common.DataServices;

namespace Persistence.Profiles
{
    public class ProfilesDataService : EntityDataService<Profile>, IProfilesDataService
    {
        public ProfilesDataService(DataContext dataContext) : base(dataContext)
        {
        }
        
        public async Task<Profile> GetByUsernameAsync(string username, bool throwExceptionIfNull = true)
        {
            var profile = await this.DataSet
                .Include(p => p.Picture)
                .FirstOrDefaultAsync(p => p.UserName == username);

            if (throwExceptionIfNull && profile == null)
            {
                throw new KeyNotFoundException(
                    ProfileErrorMessages.ProfileDoesNotExist);
            }

            return profile;
        }
    }
}
