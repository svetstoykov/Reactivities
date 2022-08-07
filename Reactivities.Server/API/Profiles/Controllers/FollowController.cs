using System.Collections.Generic;
using System.Threading.Tasks;
using API.Common.Controllers;
using API.Profiles.Models;
using Application.Profiles.Commands;
using Application.Profiles.Models;
using Application.Profiles.Queries;
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
            => this.HandleResult(await this.Mediator.Send(new FollowToggle.Command(username)));

        [HttpGet("{username}")]
        public async Task<IActionResult> GetFollowings(string username, bool getFollowers)
            => this.HandleMappingResult<IEnumerable<ProfileOutputModel>, IEnumerable<ProfileApiModel>>(
                await this.Mediator.Send(new GetFollowings.Query(username, getFollowers)));
    }
}