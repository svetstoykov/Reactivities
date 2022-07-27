using System.Threading;
using System.Threading.Tasks;
using Application.Common.Identity.Models;
using Application.Common.Identity.Models.Output;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models.Common;

namespace Application.Common.Identity
{
    public class Login : IRequest<Result<UserOutputModel>>
    {
        public Login(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class Handler : IRequestHandler<Login, Result<UserOutputModel>>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public Handler(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }


        public async Task<Result<UserOutputModel>> Handle(Login request, CancellationToken cancellationToken)
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

                return Result<UserOutputModel>.Success(userOutputModel);
            }

            return Result<UserOutputModel>.Unauthorized();
        }
    }
}
