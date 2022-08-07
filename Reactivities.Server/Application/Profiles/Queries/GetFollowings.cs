using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Profiles.DataServices;
using Application.Profiles.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.Common;

namespace Application.Profiles.Queries
{
    public class GetFollowings
    {
        public class Query : IRequest<Result<IEnumerable<ProfileOutputModel>>>
        {
            public Query(string username, bool getFollowers)
            {
                this.Username = username;
                this.GetFollowers = getFollowers;
            }
            
            public string Username { get; }
            public bool GetFollowers { get; }
        }
        
        public class Handler : IRequestHandler<Query, Result<IEnumerable<ProfileOutputModel>>>
        {
            private readonly IProfilesDataService _profilesDataService;
            private readonly IMapper _mapper;
            
            public Handler(IProfilesDataService profilesDataService, IMapper mapper)
            {
                this._profilesDataService = profilesDataService;
                this._mapper = mapper;
            }

            public async Task<Result<IEnumerable<ProfileOutputModel>>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request.GetFollowers)
                {
                    var followers = await this._profilesDataService
                        .GetProfileFollowingsAsQueryable()
                        .Where(pf => pf.Target.UserName == request.Username)
                        .Select(pf => pf.Observer)
                        .ProjectTo<ProfileOutputModel>(this._mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);
                    
                    return Result<IEnumerable<ProfileOutputModel>>
                        .Success(followers);
                }
                
                var followings = await this._profilesDataService
                    .GetProfileFollowingsAsQueryable()
                    .Where(pf => pf.Observer.UserName == request.Username)
                    .Select(pf => pf.Target)
                    .ProjectTo<ProfileOutputModel>(this._mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                
                return Result<IEnumerable<ProfileOutputModel>>
                    .Success(followings);
            }
        }
    }
}