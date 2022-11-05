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

        public string DisplayName { get; init; }

        public string Username { get; init; }

        public string Password { get; init; }

        public string Email { get; init; }
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