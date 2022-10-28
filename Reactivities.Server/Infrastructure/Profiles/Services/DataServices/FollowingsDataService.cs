using System.Linq.Expressions;
using Application.Profiles.Interfaces;
using Application.Profiles.Interfaces.DataServices;
using Application.Profiles.Models.Output;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Profiles;
using Infrastructure.Common.DataServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Profiles.Services.DataServices;

public class FollowingsDataService : EntityDataService<ProfileFollowing>, IFollowingsDataService
{
    private readonly IMapper _mapper;
    private readonly IProfileAccessor _profileAccessor;

    public FollowingsDataService(DataContext dataContext, IMapper mapper, IProfileAccessor profileAccessor) 
        : base(dataContext)
    {
        this._mapper = mapper;
        this._profileAccessor = profileAccessor;
    }

    public async Task<List<ProfileOutputModel>> GetFollowEntitiesToListAsync<TTargetResult>(
        Expression<Func<ProfileFollowing, bool>> filter,
        Expression<Func<ProfileFollowing, TTargetResult>> selector,
        CancellationToken cancellationToken)
        => await this.DataSet
            .Where(filter)
            .Select(selector)
            .ProjectTo<ProfileOutputModel>(this._mapper.ConfigurationProvider,
                new { currentProfile = this._profileAccessor.GetLoggedInUsername() })
            .ToListAsync(cancellationToken);
}