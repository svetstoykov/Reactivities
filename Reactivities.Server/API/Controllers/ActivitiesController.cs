using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models.Activities;
using Application.Activities;
using Application.Activities.Models.Input;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        public ActivitiesController(IMediator mediator, IMapper mapper)
            : base(mediator, mapper)
        { }

        [HttpGet]
        public async Task<ActionResult<List<ActivityViewModel>>> GetActivities()
        {
            var serviceResultData = await base.Mediator.Send(new List.Query());

            return base.Ok(Mapper.Map<IEnumerable<ActivityViewModel>>(serviceResultData));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityViewModel>> GetActivity(int id)
        {
            var serviceResultData = await base.Mediator.Send(new Details.Query(id));

            return base.Ok(Mapper.Map<ActivityViewModel>(serviceResultData));
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(ActivityViewModel request)
        {
            var inputModel = Mapper.Map<CreateActivityInputModel>(request);

            return base.Ok(await base.Mediator.Send(new Create.Command(inputModel)));
        }

        [HttpPut]
        public async Task<IActionResult> EditActivity(ActivityViewModel request)
        {
            var inputModel = Mapper.Map<EditActivityInputModel>(request);

            return base.Ok(await base.Mediator.Send(new Edit.Command(inputModel)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
            => base.Ok(await base.Mediator.Send(new Delete.Command(id)));
    }
}
