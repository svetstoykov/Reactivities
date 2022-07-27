using System.Threading.Tasks;
using API.Common.Controllers;
using API.Common.Identity.Models;
using Application.Common.Identity;
using Application.Common.Identity.Models.Output;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Common.Identity
{
    public class AccountController : BaseApiController
    {
        public AccountController(IMediator mediator, IMapper mapper) 
            : base(mediator, mapper)
        {
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserApiModel>> Login(LoginApiModel request)
        {
            var loginResult = await this.Mediator.Send(new Login(request.Email, request.Password));

            return base.HandleMappingResult<UserOutputModel, UserApiModel>(loginResult);
        }
    }
}
