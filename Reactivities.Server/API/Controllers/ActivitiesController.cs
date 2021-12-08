using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Activities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models.Activities.Request;
using Models.Activities.Response;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        public ActivitiesController(IMediator mediator) 
            : base(mediator)
        { }

        [HttpGet]
        public async Task<ActionResult<List<ActivityResponse>>> GetActivities()
        {
            return await base.Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public  async  Task<ActionResult<ActivityResponse>> GetActivity(int id)
        {
            return await base.Mediator.Send(new Details.Query(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(CreateActivityRequest activity)
        {
            return base.Ok(await base.Mediator.Send(new Create.Command(activity)));
        }

        [HttpPut]
        public async Task<IActionResult> EditActivity(EditActivityRequest activity)
        {
            return base.Ok(await base.Mediator.Send(new Edit.Command(activity)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            return base.Ok(await base.Mediator.Send(new Delete.Command(id)));
        }
    }
}
