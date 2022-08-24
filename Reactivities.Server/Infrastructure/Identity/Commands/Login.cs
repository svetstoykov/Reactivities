using Application.Common.ErrorHandling;
using Infrastructure.Identity.Tokens.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models.Common;
using User = Infrastructure.Identity.Models.User;

namespace Infrastructure.Identity.Commands
{
    public class Login
    {
        public class Command: IRequest<Result<string>>
        {
            public Command(string email, string password)
            {
                this.Email = email;
                this.Password = password;
            }

            public string Email { get; init; }

            public string Password { get; init; }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;
            private readonly ITokenService _tokenService;

            public Handler(
                UserManager<User> userManager,
                SignInManager<User> signInManager,
                ITokenService tokenService)
            {
                this._userManager = userManager;
                this._signInManager = signInManager;
                this._tokenService = tokenService;
            }

            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await this._userManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    return Result<string>.Unauthorized(
                        CommonErrorMessages.InvalidEmail);
                }
                
                var signInResult = await this._signInManager
                    .CheckPasswordSignInAsync(user, request.Password, false);

                if (signInResult.Succeeded)
                {
                    return Result<string>.Success(this._tokenService.GenerateToken(user));
                }

                return Result<string>.Unauthorized(
                    CommonErrorMessages.FailedLogin);
            }
        }
    }
}