using System.Threading;
using System.Threading.Tasks;
using Application.Common.Identity.Models;
using Application.Common.Identity.Models.Output;
using Application.Common.Identity.Tokens.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models.Common;
using Models.ErrorHandling.Helpers;

namespace Application.Common.Identity
{
    public class GetCurrentUser
    {
        public class Query : IRequest<Result<UserOutputModel>>
        {
            public string Email { get; }

            public Query(string email)
            {
                Email = email;
            }
        }

        public class Handler : IRequestHandler<Query, Result<UserOutputModel>>
        {
            private readonly UserManager<User> _userManager;
            private readonly ITokenService _tokenService;
            private readonly IMapper _mapper;

            public Handler(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
            {
                _userManager = userManager;
                _tokenService = tokenService;
                _mapper = mapper;
            }

            public async Task<Result<UserOutputModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    return Result<UserOutputModel>.NotFound(
                        IdentityErrorMessages.InvalidCurrentUser);
                }

                var userOutputModel = _mapper.Map<UserOutputModel>(user);

                userOutputModel.Token = _tokenService.GenerateToken(user);

                return Result<UserOutputModel>.Success(userOutputModel);
            }
        }
    }
}
