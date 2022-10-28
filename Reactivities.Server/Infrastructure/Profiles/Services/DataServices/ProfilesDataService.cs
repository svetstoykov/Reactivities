using Application.Profiles.ErrorHandling;
using Application.Profiles.Interfaces;
using Application.Profiles.Interfaces.DataServices;
using Application.Profiles.Models.Output;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Common.DataServices;
using Microsoft.EntityFrameworkCore;
using Profile = Domain.Profiles.Profile;

namespace Infrastructure.Profiles.Services.DataServices;

public class ProfilesDataService : EntityDataService<Profile>, IProfilesDataService
{
    private readonly IMapper _mapper;
    private readonly IProfileAccessor _profileAccessor;

    public ProfilesDataService(DataContext dataContext, IMapper mapper, IProfileAccessor profileAccessor) 
        : base(dataContext)
    {
        this._mapper = mapper;
        this._profileAccessor = profileAccessor;
    }

    public async Task<Profile> GetByUsernameAsync(string username, bool throwExceptionIfNull = true)
    {
        var profile = await this.DataSet
            .Include(p => p.Picture)
            .FirstOrDefaultAsync(p => p.UserName == username);

        if (throwExceptionIfNull && profile == null)
        {
            throw new KeyNotFoundException(string.Format(
                ProfileErrorMessages.ProfileDoesNotExist, username));
        }

        return profile;
    }

    public async Task<ProfileOutputModel> GetProfileOutputModel(
        string username, CancellationToken cancellationToken = default)
        => await this.DataSet
            .ProjectTo<ProfileOutputModel>(this._mapper.ConfigurationProvider,
                new { currentProfile = this._profileAccessor.GetLoggedInUsername() })
            .FirstOrDefaultAsync(p => p.Username == username, cancellationToken);

    public async Task<Profile> GetProfileWithFollowings(string username, CancellationToken cancellationToken)
        => await this.DataSet
            .Include(p => p.Followings)
            .ThenInclude(pf => pf.Target)
            .FirstOrDefaultAsync(p => p.UserName == username, cancellationToken);
}