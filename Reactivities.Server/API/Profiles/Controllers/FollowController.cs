using System.Threading.Tasks;
using API.Common.Controllers;
using Application.Profiles.Commands;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Profiles.Controllers
{
    public class FollowController : BaseApiController
    {
        public FollowController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost("{username}")]
        public async Task<IActionResult> Follow(string username)
            =>this.HandleResult(await this.Mediator.Send(new FollowToggle.Command(
                this.GetCurrentUserUsername, username)));
    }
}