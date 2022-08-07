using System.Threading;
using System.Threading.Tasks;
using Application.Profiles.DataServices;
using Application.Profiles.Models;
using Application.Profiles.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            private readonly IProfileAccessor _profileAccessor;
            private readonly IMapper _mapper;

            public Handler(IProfilesDataService profilesDataService,IProfileAccessor profileAccessor, IMapper mapper)
            {
                this._profilesDataService = profilesDataService;
                this._profileAccessor = profileAccessor;
                this._mapper = mapper;
            }

            public async Task<Result<ProfileOutputModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var profile = await this._profilesDataService
                    .GetAsQueryable()
                    .ProjectTo<ProfileOutputModel>(this._mapper.ConfigurationProvider,
                        new {currentProfile = this._profileAccessor.GetLoggedInUsername()})
                    .FirstOrDefaultAsync(p => p.Username == request.Username, cancellationToken);

                return Result<ProfileOutputModel>.Success(profile);
            }
        }
    }

}
