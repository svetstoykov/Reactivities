using System.Threading;
using System.Threading.Tasks;
using Application.Common.Identity.Models;
using Application.Common.Identity.Models.Output;
using Application.Common.Identity.Tokens.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.ErrorHandling.Helpers;
using Profile = Domain.Profiles.Profile;

namespace Application.Common.Identity.Commands;

public class Register
{
    public class Command : IRequest<Result<UserOutputModel>>
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

    public class Handler : IRequestHandler<Command, Result<UserOutputModel>>
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

        public async Task<Result<UserOutputModel>> Handle(Command request, CancellationToken cancellationToken)
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
                var userOutputModel = this._mapper.Map<UserOutputModel>(user);
                userOutputModel.Token = this._tokenService.GenerateToken(user);

                return Result<UserOutputModel>.Success(userOutputModel);
            }

            return Result<UserOutputModel>.Failure(
                IdentityErrorMessages.FailedToCreateUser);
        }

        private async Task<Result<UserOutputModel>> ValidateUserDetails(Command request)
        {
            if (await this._userManager.Users.AnyAsync(u => 
                    u.Email.ToLower() == request.Email.ToLower()))
            {
                return Result<UserOutputModel>.Failure(
                    string.Format(IdentityErrorMessages.EmailTaken, request.Email));
            }

            if (await this._userManager.Users.AnyAsync(u => 
                    u.UserName.ToLower() == request.Username.ToLower()))
            {
                return Result<UserOutputModel>.Failure(
                    string.Format(IdentityErrorMessages.UsernameTaken, request.Username));
            }

            return Result<UserOutputModel>.Success(null);
        }
    }
}