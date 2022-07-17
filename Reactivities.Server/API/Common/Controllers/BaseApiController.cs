using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models.Common;

namespace API.Common.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected readonly IMediator Mediator;
        protected readonly IMapper Mapper;

        public BaseApiController(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator;
            Mapper = mapper;
        }

        protected ActionResult HandleResult<TOutputData, TViewModel>(Result<TOutputData> result)
            where TOutputData : class
            where TViewModel : class
        {
            var mappedData = this.Mapper.Map<TViewModel>(result.Data);

            return ProvideActionResultResponse(result.IsSuccessful, mappedData, result.Message);
        }

        protected ActionResult HandleResult<TOutputData>(Result<TOutputData> result)
        {
            return ProvideActionResultResponse(result.IsSuccessful, result.Data, result.Message);
        }

        private ActionResult ProvideActionResultResponse<TData>(bool isSuccessful, TData data, string message)
            => isSuccessful switch
            {
                true when data != null => Ok(data),
                true => NotFound(),
                _ => BadRequest(message)
            };
    }
}
