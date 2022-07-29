using System.Threading;
using System.Threading.Tasks;
using Application.Common.Identity.Models.Output;
using Application.Common.Identity.Tokens.Interfaces;
using AutoMapper;
using Domain.Common.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models.Common;
using Models.ErrorHandling.Helpers;

namespace Application.Common.Identity.Commands;

public class Login
{
    public class Command: IRequest<Result<UserOutputModel>>
    {
        public Command(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        public string Email { get; init; }

        public string Password { get; init; }
    }

    public class Handler : IRequestHandler<Command, Result<UserOutputModel>>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public Handler(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._tokenService = tokenService;
            this._mapper = mapper;
        }


        public async Task<Result<UserOutputModel>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await this._userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return Result<UserOutputModel>.Unauthorized(
                    IdentityErrorMessages.InvalidEmail);
            }

            var signInResult = await this._signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (signInResult.Succeeded)
            {
                var userOutputModel = this._mapper.Map<UserOutputModel>(user);

                userOutputModel.Token = this._tokenService.GenerateToken(user);

                return Result<UserOutputModel>.Success(userOutputModel);
            }

            return Result<UserOutputModel>.Unauthorized(
                IdentityErrorMessages.FailedLogin);
        }
    }
}