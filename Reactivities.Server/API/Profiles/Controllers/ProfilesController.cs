using System.Threading.Tasks;
using API.Common.Controllers;
using API.Common.Extensions;
using API.Profiles.Models;
using Application.Profiles.Commands;
using Application.Profiles.Models;
using Application.Profiles.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        [HttpPut]
        public async Task<IActionResult> UpdateDetails(ProfileApiModel request)
            => this.HandleResult( await this.Mediator.Send(new UpdateDetails.Command(
                request.DisplayName, request.Bio, request.Email, request.Username)));

        [HttpPost("uploadProfilePicture")]
        public async Task<IActionResult> UploadProfilePicture([FromForm] IFormFile file)
            => this.HandleResult(await this.Mediator.Send(new UploadProfilePicture.Command(
                await file.GetBytesAsync(), file.FileName, this.GetCurrentUserUsername())));

        [HttpDelete("deleteProfilePicture")]
        public async Task<IActionResult> DeleteProfilePicture()
            => this.HandleResult(await this.Mediator.Send(new DeleteProfilePicture.Command(
                this.GetCurrentUserUsername())));
    }
}