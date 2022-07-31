using System.Threading.Tasks;
using API.Common.Controllers;
using API.Profiles.Models;
using Application.Profiles.Models;
using Application.Profiles.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Profiles.Controllers
{
    public class ProfilesController : BaseApiController
    {
        public ProfilesController(IMediator mediator, IMapper mapper) 
            : base(mediator, mapper)
        {
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
            => this.HandleMappingResult<ProfileOutputModel, ProfileApiModel>(
                await this.Mediator.Send(new GetProfile.Query(username)));

        [HttpGet]
        public async Task<IActionResult> GetCurrentProfile()
            => this.HandleMappingResult<ProfileOutputModel, ProfileApiModel>(
                await this.Mediator.Send(new GetProfile.Query(this.GetCurrentUserUsername())));

        [HttpPost("uploadPhoto/{imageId}")]
        public async Task<IActionResult> UploadProfilePhoto(string imageId)
        {
            return this.Ok();
        }

        [HttpDelete("removePhoto/{imageId}")]
        public async Task<IActionResult> RemoveProfilePhoto(string imageId)
        {
            return this.Ok();
        }
    }
}
