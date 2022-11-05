using System.Threading;
using System.Threading.Tasks;
using Application.Identity.Interfaces;
using MediatR;
using Reactivities.Common.Result.Models;

namespace Application.Identity.Commands;

public class Login
{
    public class Command: IRequest<Result<string>>
    {
        public Command(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        public string Email { get; }

        public string Password { get; }
    }

    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly IUserIdentityService _userIdentityService;
        
        public Handler(IUserIdentityService userIdentityService)
        {
            this._userIdentityService = userIdentityService;
        }

        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var loginResult = await this._userIdentityService.LoginAndGenerateTokenAsync(
                request.Email, request.Password, cancellationToken);

            return loginResult;
        }
    }
}