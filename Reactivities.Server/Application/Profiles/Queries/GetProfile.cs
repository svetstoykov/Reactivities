using System.Threading;
using System.Threading.Tasks;
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
            public Task<Result<ProfileOutputModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }

}
