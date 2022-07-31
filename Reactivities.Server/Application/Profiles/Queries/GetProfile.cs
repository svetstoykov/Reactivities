using System.Threading;
using System.Threading.Tasks;
using Application.Profiles.DataServices;
using Application.Profiles.Models;
using AutoMapper;
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
            private readonly IMapper _mapper;

            public Handler(IProfilesDataService profilesDataService, IMapper mapper)
            {
                this._profilesDataService = profilesDataService;
                this._mapper = mapper;
            }

            public async Task<Result<ProfileOutputModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var profile = await this._profilesDataService
                    .GetAsQueryable()
                    .FirstOrDefaultAsync(p => p.UserName == request.Username);

                var outputModel = this._mapper.Map<ProfileOutputModel>(profile);

                return Result<ProfileOutputModel>.Success(outputModel);
            }
        }
    }

}
