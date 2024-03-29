﻿using System.Threading;
using System.Threading.Tasks;
using Application.Profiles.Models.Output;
using Domain.Profiles;
using Reactivities.Common.DataServices.Abstractions.Interfaces;

namespace Application.Profiles.Interfaces.DataServices;

public interface IProfilesDataService : IEntityDataService<Profile>
{
    Task<Profile> GetByUsernameAsync(
        string username, bool throwExceptionIfNull = true);

    Task<ProfileOutputModel> GetProfileOutputModel(
        string username, CancellationToken cancellationToken = default);

    Task<Profile> GetProfileWithFollowings(
        string username, CancellationToken cancellationToken);
}