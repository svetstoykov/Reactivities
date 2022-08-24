using Application.Profiles.DataServices;
using Domain.Profiles;
using Persistence.Common.DataServices;

namespace Persistence.Profiles
{
    public class FollowingsDataService : EntityDataService<ProfileFollowing>, IFollowingsDataService
    {
        public FollowingsDataService(DataContext dataContext) : base(dataContext)
        {
        }
    }
}