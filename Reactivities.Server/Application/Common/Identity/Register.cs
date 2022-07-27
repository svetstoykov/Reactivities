﻿using System;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Application.Common.Identity;

public class Register
{
    public class Command : IRequest<Result<UserOutputModel>>
    {
        public Command(string displayName, string username, string password, string email)
        {
            DisplayName = displayName;
            Username = username;
            Password = password;
            Email = email;
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
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<Result<UserOutputModel>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await ValidateUserDetails(request);
            if (!validationResult.IsSuccessful)
            {
                return validationResult;
            }

            var user = User.New(
                request.Username, request.Email, request.DisplayName);

            var registerUser = await _userManager.CreateAsync(user, request.Password);

            if (registerUser.Succeeded)
            {
                var userOutputModel = _mapper.Map<UserOutputModel>(user);
                userOutputModel.Token = _tokenService.GenerateToken(user);

                return Result<UserOutputModel>.Success(userOutputModel);
            }

            return Result<UserOutputModel>.Failure(
                IdentityErrorMessages.FailedToCreateUser);
        }

        private async Task<Result<UserOutputModel>> ValidateUserDetails(Command request)
        {
            if (await _userManager.Users.AnyAsync(u => 
                    u.Email.ToLower() == request.Email.ToLower()))
            {
                return Result<UserOutputModel>.Failure(
                    string.Format(IdentityErrorMessages.EmailTaken, request.Email));
            }

            if (await _userManager.Users.AnyAsync(u => 
                    u.UserName.ToLower() == request.Username.ToLower()))
            {
                return Result<UserOutputModel>.Failure(
                    string.Format(IdentityErrorMessages.UsernameTaken, request.Username));
            }

            return Result<UserOutputModel>.Success(null);
        }
    }
}