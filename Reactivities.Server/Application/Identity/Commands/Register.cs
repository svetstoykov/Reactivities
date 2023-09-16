using System.Threading;
using System.Threading.Tasks;
using Application.Identity.Interfaces;
using MediatR;
using Reactivities.Common.Result.Models;

namespace Application.Identity.Commands;

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

        public string DisplayName { get; }

        public string Username { get; }

        public string Password { get; }

        public string Email { get; }
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
            var registrationResult = await this._userIdentityService.RegisterAndGenerateTokenAsync(
                request.Username, request.Email, request.Password, request.DisplayName, cancellationToken);

            return registrationResult;
        }
    }
}