using System.Threading;
using System.Threading.Tasks;
using Application.Profiles.DataServices;
using Application.Profiles.Models;
using MediatR;
using Models.Common;

namespace Application.Profiles.Queries
{
    public class GetProfile
    {
        public class Query : IRequest<Result<ProfileOutputModel>>
        {
            public Query(string username)
            {
                this.Username = username;
            }

            public string Username { get; }
        }

        public class Handler : IRequestHandler<Query, Result<ProfileOutputModel>>
        {
            private readonly IProfilesDataService _profilesDataService;

            public Handler(IProfilesDataService profilesDataService)
            {
                this._profilesDataService = profilesDataService;
            }

            public async Task<Result<ProfileOutputModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var profile = await this._profilesDataService
                    .GetProfileOutputModel(request.Username, cancellationToken);

                return Result<ProfileOutputModel>.Success(profile);
            }
        }
    }

}
