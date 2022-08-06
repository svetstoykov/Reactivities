using System.Threading;
using System.Threading.Tasks;
using Application.Common.ErrorHandling;
using Application.Common.Identity.Models;
using Application.Common.Identity.Tokens.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Profile = Domain.Profiles.Profile;

namespace Application.Common.Identity.Commands
{
    public class Register
    {
        public class Command : IRequest<Result<string>>
        {
            public Command(string displayName, string username, string password, string email)
            {
                this.DisplayName = displayName;
                this.Username = username;
                this.Password = password;
                this.Email = email;
            }

            public string DisplayName { get; init; }

            public string Username { get; init; }

            public string Password { get; init; }

            public string Email { get; init; }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly UserManager<User> _userManager;
            private readonly ITokenService _tokenService;
            private readonly IMapper _mapper;

            public Handler(UserManager<User> userManager, ITokenService tokenService,IMapper mapper)
            {
                this._userManager = userManager;
                this._tokenService = tokenService;
                this._mapper = mapper;
            }

            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await this.ValidateUserDetails(request);
                if (!validationResult.IsSuccessful)
                {
                    return validationResult;
                }

                var profile = Profile.New(
                    request.Username, request.Email, request.DisplayName);

                var user = this._mapper.Map<User>(profile);

                var registerUser = await this._userManager.CreateAsync(user, request.Password);

                if (registerUser.Succeeded)
                {
                    return Result<string>.Success(
                        this._tokenService.GenerateToken(user));
                }

                return Result<string>.Failure(
                    CommonErrorMessages.FailedToCreateUser);
            }

            private async Task<Result<string>> ValidateUserDetails(Command request)
            {
                if (await this._userManager.Users.AnyAsync(u => 
                        u.Email.ToLower() == request.Email.ToLower()))
                {
                    return Result<string>.Failure(
                        string.Format(CommonErrorMessages.EmailTaken, request.Email));
                }

                if (await this._userManager.Users.AnyAsync(u => 
                        u.UserName.ToLower() == request.Username.ToLower()))
                {
                    return Result<string>.Failure(
                        string.Format(CommonErrorMessages.UsernameTaken, request.Username));
                }

                return Result<string>.Success(null);
            }
        }
    }
}