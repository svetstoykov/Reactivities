using System.Threading.Tasks;
using API.Common.Controllers;
using API.Common.Identity.Models;
using Application.Identity.Commands;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Common.Identity;

[AllowAnonymous]
public class AccountsController : BaseApiController
{
    public AccountsController(IMediator mediator, IMapper mapper)
        : base(mediator, mapper)
    {
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginApiModel request)
        => this.HandleResult(await this.Mediator.Send(
            new Login.Command(request.Email, request.Password)));

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterApiModel request)
        => this.HandleResult(await this.Mediator.Send(
            new Register.Command(request.DisplayName, request.Username, request.Password, request.Email)));
}