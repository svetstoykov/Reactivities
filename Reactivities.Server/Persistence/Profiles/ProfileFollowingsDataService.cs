using Application.Profiles.DataServices;
using Domain.Profiles;
using Persistence.Common.DataServices;

namespace Persistence.Profiles;

public class ProfileFollowingsDataService : EntityDataService<ProfileFollowing>, IProfileFollowingsDataService
{
    public ProfileFollowingsDataService(DataContext dataContext) : base(dataContext)
    {
    }
}