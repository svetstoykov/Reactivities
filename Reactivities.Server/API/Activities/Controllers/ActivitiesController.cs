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
        public async Task<ActionResult<IEnumerable<ActivityApiModel>>> GetActivities()
        {
            var serviceResult = await base.Mediator.Send(new List.Query());

            return HandleMappingResult<IEnumerable<ActivityOutputModel>, IEnumerable<ActivityApiModel>>(serviceResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityApiModel>> GetActivity(int id)
        {
            var serviceResult = await base.Mediator.Send(new Details.Query(id));

            return HandleMappingResult<ActivityOutputModel, ActivityApiModel>(serviceResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(ActivityApiModel request)
        {
            var inputModel = Mapper.Map<CreateActivityInputModel>(request);

            var serviceResult = await base.Mediator.Send(new Create.Command(inputModel));

            return HandleResult(serviceResult);
        }

        [HttpPut]
        public async Task<IActionResult> EditActivity(ActivityApiModel request)
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

        [HttpGet("categories")]
        public async Task<IActionResult> GetActivityCategories()
        {
            var serviceResult = await base.Mediator.Send(new Categories.Query());

            return HandleMappingResult<IEnumerable<CategoryOutputModel>, IEnumerable<CategoryApiModel>>(serviceResult);
        }
    }
}
