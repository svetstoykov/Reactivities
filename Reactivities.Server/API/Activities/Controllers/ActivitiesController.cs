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
            var serviceResult = await this.Mediator.Send(new List.Query());

            return this.HandleMappingResult<IEnumerable<ActivityOutputModel>, IEnumerable<ActivityApiModel>>(serviceResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityApiModel>> GetActivity(int id)
        {
            var serviceResult = await this.Mediator.Send(new Details.Query(id));

            return this.HandleMappingResult<ActivityOutputModel, ActivityApiModel>(serviceResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(ActivityApiModel request)
        {
            var inputModel = this.Mapper.Map<CreateActivityInputModel>(request);

            var serviceResult = await this.Mediator.Send(new Create.Command(inputModel));

            return this.HandleResult(serviceResult);
        }

        [HttpPut]
        public async Task<IActionResult> EditActivity(ActivityApiModel request)
        {
            var inputModel = this.Mapper.Map<EditActivityInputModel>(request);

            var serviceResult = await this.Mediator.Send(new Edit.Command(inputModel));

            return this.HandleResult(serviceResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var serviceResult = await this.Mediator.Send(new Delete.Command(id));

            return this.HandleResult(serviceResult);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetActivityCategories()
        {
            var serviceResult = await this.Mediator.Send(new Categories.Query());

            return this.HandleMappingResult<IEnumerable<CategoryOutputModel>, IEnumerable<CategoryApiModel>>(serviceResult);
        }
    }
}
