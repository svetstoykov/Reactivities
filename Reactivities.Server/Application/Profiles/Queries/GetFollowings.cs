using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Profiles.Interfaces;
using Application.Profiles.Interfaces.DataServices;
using Application.Profiles.Models.Output;
using MediatR;
using Reactivities.Common.Result.Models;

namespace Application.Profiles.Queries;

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
            
        public Handler(IFollowingsDataService followingsDataService, IProfileAccessor profileAccessor)
        {
            this._followingsDataService = followingsDataService;
        }

        public async Task<Result<IEnumerable<ProfileOutputModel>>> Handle(Query request, CancellationToken cancellationToken)
        {

            if (request.ReturnFollowersInsteadOfFollowings)
            {
                var followers = await this._followingsDataService
                    .GetFollowEntitiesToListAsync(
                        p => p.Target.UserName == request.Username,
                        p => p.Observer,
                        cancellationToken);
                    
                return Result<IEnumerable<ProfileOutputModel>>
                    .Success(followers);
            }
                
            var followings = await this._followingsDataService
                .GetFollowEntitiesToListAsync(
                    p => p.Observer.UserName == request.Username,
                    p => p.Target,
                    cancellationToken);
                
            return Result<IEnumerable<ProfileOutputModel>>
                .Success(followings);
        }
    }
}