using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DataServices;
using Application.Profiles.Models;
using Domain.Profiles;

namespace Application.Profiles.DataServices
{
    public interface IFollowingsDataService : IEntityDataService<ProfileFollowing>
    {
        Task<List<ProfileOutputModel>> GetFollowEntitiesToListAsync<TTargetResult>(
            Expression<Func<ProfileFollowing, bool>> filter,
            Expression<Func<ProfileFollowing, TTargetResult>> selector,
            CancellationToken cancellationToken);
    }
}