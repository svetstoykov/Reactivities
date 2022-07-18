using System.Collections.Generic;
using System.Threading.Tasks;
using API.Activities.Models;
using API.Common.Controllers;
using Application.Activities;
using Application.Activities.Models.Input;
using Application.Activities.Models.Output;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Activities.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        public ActivitiesController(IMediator mediator, IMapper mapper)
            : base(mediator, mapper)
        { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityViewModel>>> GetActivities()
        {
            var serviceResult = await base.Mediator.Send(new List.Query());

            return HandleMappingResult<IEnumerable<ActivityOutputModel>, IEnumerable<ActivityViewModel>>(serviceResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityViewModel>> GetActivity(int id)
        {
            var serviceResult = await base.Mediator.Send(new Details.Query(id));

            return HandleMappingResult<ActivityOutputModel, ActivityViewModel>(serviceResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(ActivityViewModel request)
        {
            var inputModel = Mapper.Map<CreateActivityInputModel>(request);

            var serviceResult = await base.Mediator.Send(new Create.Command(inputModel));

            return HandleResult(serviceResult);
        }

        [HttpPut]
        public async Task<IActionResult> EditActivity(ActivityViewModel request)
        {
            var inputModel = Mapper.Map<EditActivityInputModel>(request);

            var serviceResult = await base.Mediator.Send(new Edit.Command(inputModel));

            return HandleResult(serviceResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var serviceResult = await base.Mediator.Send(new Delete.Command(id));

            return HandleResult(serviceResult);
        }
    }
}
