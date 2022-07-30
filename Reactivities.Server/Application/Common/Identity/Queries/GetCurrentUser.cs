using System.Threading;
using System.Threading.Tasks;
using Application.Common.Identity.Models.Output;
using Application.Common.Identity.Tokens.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models.Common;
using Models.ErrorHandling.Helpers;
using User = Application.Common.Identity.Models.Base.User;

namespace Application.Common.Identity.Queries
{
    public class GetCurrentUser
    {
        public class Query : IRequest<Result<UserOutputModel>>
        {
            public Query(string email)
            {
                this.Email = email;
            }

            public string Email { get; init; }
        }

        public class Handler : IRequestHandler<Query, Result<UserOutputModel>>
        {
            private readonly UserManager<User> _userManager;
            private readonly ITokenService _tokenService;
            private readonly IMapper _mapper;

            public Handler(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
            {
                this._userManager = userManager;
                this._tokenService = tokenService;
                this._mapper = mapper;
            }

            public async Task<Result<UserOutputModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await this._userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    return Result<UserOutputModel>.NotFound(
                        IdentityErrorMessages.InvalidCurrentUser);
                }

                var userOutputModel = this._mapper.Map<UserOutputModel>(user);

                userOutputModel.Token = this._tokenService.GenerateToken(user);

                return Result<UserOutputModel>.Success(userOutputModel);
            }
        }
    }
}
