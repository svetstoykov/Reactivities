using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Common;
using Models.Enumerations;

namespace API.Common.Controllers
{
    [Authorize]
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

        protected ActionResult HandleMappingResult<TOutputData, TViewModel>(Result<TOutputData> result)
            where TOutputData : class
            where TViewModel : class
        {
            Result<TViewModel> mappedResult = null;
            if (result != null)
            {
                mappedResult = Result<TViewModel>.New(
                    this.Mapper.Map<TViewModel>(result.Data),
                    result.ResultType,
                    result.IsSuccessful,
                    result.Message);
            }

            return HandleResult(mappedResult);
        }

        protected ActionResult HandleResult<TOutputData>(Result<TOutputData> result)
        {
            return result.ResultType switch
            {
                ResultType.Success when result.Data == null => NotFound(),
                ResultType.NotFound => NotFound(),
                ResultType.Success => Ok(result.Data),
                ResultType.Unauthorized => Unauthorized(),
                _ => BadRequest(result.Message)
            };
        }
    }
}
