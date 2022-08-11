using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Profiles.DataServices;
using Application.Profiles.Models;
using Application.Profiles.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Profiles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.Common;

namespace Application.Profiles.Queries
{
    public class GetFollowings
    {
        public class Query : IRequest<Result<IEnumerable<ProfileOutputModel>>>
        {
            public Query(string username, bool returnFollowersInsteadOfFollowings)
            {
                this.Username = username;
                this.ReturnFollowersInsteadOfFollowings = returnFollowersInsteadOfFollowings;
            }
            
            public string Username { get; }
            public bool ReturnFollowersInsteadOfFollowings { get; }
        }
        
        public class Handler : IRequestHandler<Query, Result<IEnumerable<ProfileOutputModel>>>
        {
            private readonly IFollowingsDataService _followingsDataService;
            private readonly IProfileAccessor _profileAccessor;
            private readonly IMapper _mapper;
            
            public Handler(IFollowingsDataService followingsDataService, IProfileAccessor profileAccessor, IMapper mapper)
            {
                this._followingsDataService = followingsDataService;
                this._profileAccessor = profileAccessor;
                this._mapper = mapper;
            }

            public async Task<Result<IEnumerable<ProfileOutputModel>>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request.ReturnFollowersInsteadOfFollowings)
                {
                    var followers = await this.GetFollowEntitiesToListAsync(
                        (p) => p.Target.UserName == request.Username,
                        (p) => p.Observer,
                        cancellationToken);
                    
                    return Result<IEnumerable<ProfileOutputModel>>
                        .Success(followers);
                }
                
                var followings = await this.GetFollowEntitiesToListAsync(
                    (p) => p.Observer.UserName == request.Username,
                    (p) => p.Target,
                    cancellationToken);
                
                return Result<IEnumerable<ProfileOutputModel>>
                    .Success(followings);
            }

            private async Task<List<ProfileOutputModel>> GetFollowEntitiesToListAsync<TTargetResult>(
                Expression<Func<ProfileFollowing, bool>> filter,
                Expression<Func<ProfileFollowing, TTargetResult>> selector,
                CancellationToken cancellationToken)
            {
                return await this._followingsDataService
                    .GetAsQueryable().Where(filter)
                    .Select(selector)
                    .ProjectTo<ProfileOutputModel>(this._mapper.ConfigurationProvider,
                        new {currentProfile = this._profileAccessor.GetLoggedInUsername()})
                    .ToListAsync(cancellationToken);
            }
        }
    }
}