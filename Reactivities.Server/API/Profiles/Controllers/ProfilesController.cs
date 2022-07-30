using System.Threading.Tasks;
using API.Common.Controllers;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(string id)
        {
            return this.Ok();
        }

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
