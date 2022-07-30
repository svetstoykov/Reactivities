using System.Threading.Tasks;
using API.Common.Controllers;
using API.Common.Identity.Models;
using Application.Common.Identity.Commands;
using Application.Common.Identity.Models.Output;
using Application.Common.Identity.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Common.Identity
{
    [AllowAnonymous]
    public class AccountsController : BaseApiController
    {
        public AccountsController(IMediator mediator, IMapper mapper)
            : base(mediator, mapper)
        {
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginApiModel request)
            => this.HandleMappingResult<UserOutputModel, UserApiModel>(await this.Mediator.Send(
                new Login.Command(request.Email, request.Password)));

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterApiModel request)
            => this.HandleMappingResult<UserOutputModel, UserApiModel>(await this.Mediator.Send(
                new Register.Command(request.DisplayName, request.Username, request.Password, request.Email)));
        
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
            => this.HandleMappingResult<UserOutputModel, UserApiModel>(await this.Mediator.Send(
                new GetCurrentUser.Query(this.GetCurrentUserEmail())));
    }
}
