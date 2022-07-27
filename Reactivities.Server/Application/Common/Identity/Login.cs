using System.Threading;
using System.Threading.Tasks;
using Application.Common.Identity.Models;
using Application.Common.Identity.Models.Output;
using Application.Common.Identity.Tokens.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models.Common;

namespace Application.Common.Identity;

public class Login
{
    public class Command: IRequest<Result<UserOutputModel>>
    {
        public Command(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }

        public string Password { get; set; }
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
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }


        public async Task<Result<UserOutputModel>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return Result<UserOutputModel>.Unauthorized();
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (signInResult.Succeeded)
            {
                var userOutputModel = _mapper.Map<UserOutputModel>(user);

                userOutputModel.Token = _tokenService.GenerateToken(user);

                return Result<UserOutputModel>.Success(userOutputModel);
            }

            return Result<UserOutputModel>.Unauthorized();
        }
    }
}