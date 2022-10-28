using Application.Common.ErrorHandling;
using Application.Identity.Interfaces;
using AutoMapper;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Tokens.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Profile = Domain.Profiles.Profile;

namespace Infrastructure.Identity.Services;

public class UserIdentityService : IUserIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public UserIdentityService(
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

    public async Task<Result<string>> LoginAndGenerateTokenAsync(
        string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await this._userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return Result<string>.Unauthorized(
                CommonErrorMessages.InvalidEmail);
        }

        var signInResult = await this._signInManager
            .CheckPasswordSignInAsync(user, password, false);

        if (signInResult.Succeeded)
        {
            return Result<string>.Success(this._tokenService.GenerateToken(user));
        }

        return Result<string>.Unauthorized(
            CommonErrorMessages.FailedLogin);
    }

    public async Task<Result<string>> RegisterAndGenerateTokenAsync(
        string username, string email, string password, string displayName, CancellationToken cancellationToken = default)
    {
        var validationResult = await this.ValidateDuplicateUserDetails(username, email);
        if (!validationResult.IsSuccessful)
        {
            return validationResult;
        }

        var profile = Profile.New(username, email, displayName);

        var user = this._mapper.Map<User>(profile);

        var registerUser = await this._userManager.CreateAsync(user, password);

        if (registerUser.Succeeded)
        {
            return Result<string>.Success(
                this._tokenService.GenerateToken(user));
        }

        return Result<string>.Failure(
            CommonErrorMessages.FailedToCreateUser);
    }
    
    private async Task<Result<string>> ValidateDuplicateUserDetails(string username, string email)
    {
        if (await this._userManager.Users.AnyAsync(u => 
                u.Email.ToLower() == email.ToLower()))
        {
            return Result<string>.Failure(
                string.Format(CommonErrorMessages.EmailTaken, email));
        }

        if (await this._userManager.Users.AnyAsync(u => 
                u.UserName.ToLower() == username.ToLower()))
        {
            return Result<string>.Failure(
                string.Format(CommonErrorMessages.UsernameTaken, username));
        }

        return Result<string>.Success(null);
    }
}