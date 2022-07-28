using System.Security.Claims;
using System.Threading.Tasks;
using API.Common.Controllers;
using API.Common.Identity.Models;
using Application.Common.Identity;
using Application.Common.Identity.Models.Output;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Common.Identity
{
    public class AccountsController : BaseApiController
    {
        public AccountsController(IMediator mediator, IMapper mapper)
            : base(mediator, mapper)
        {
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserApiModel>> Login(LoginApiModel request)
        {
            var loginResult = await this.Mediator.Send(
                new Login.Command(request.Email, request.Password));

            return this.HandleMappingResult<UserOutputModel, UserApiModel>(loginResult);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserApiModel>> Register(RegisterApiModel request)
        {
            var registrationResult = await this.Mediator.Send(
                new Register.Command(request.DisplayName, request.Username, request.Password, request.Email));

            return this.HandleMappingResult<UserOutputModel, UserApiModel>(registrationResult);
        }

        [HttpGet]
        public async Task<ActionResult<UserApiModel>> GetCurrentUser()
        {
            var currentUserEmail = this.User.FindFirstValue(ClaimTypes.Email);

            var currentUserResult = await this.Mediator.Send(
                new GetCurrentUser.Query(currentUserEmail));

            return this.HandleMappingResult<UserOutputModel, UserApiModel>(currentUserResult);
        }
    }
}
