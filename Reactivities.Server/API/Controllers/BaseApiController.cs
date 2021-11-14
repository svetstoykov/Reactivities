using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected IMediator Mediator { get; }

        public BaseApiController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
