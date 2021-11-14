using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        public ActivitiesController(IMediator mediator) 
            : base(mediator)
        { }

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return await base.Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public  async  Task<ActionResult<Activity>> GetActivity(int id)
        {
            return await base.Mediator.Send(new Details.Query(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            return base.Ok(await base.Mediator.Send(new Create.Command(activity)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditActivity(int id, Activity activity)
        {
            activity.Id = id;

            return base.Ok(await base.Mediator.Send(new Edit.Command(activity)));
        }
    }
}
