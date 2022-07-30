using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Activities.Models;
using API.Common.Controllers;
using Application.Activities.Commands;
using Application.Activities.Models.Base;
using Application.Activities.Models.Input;
using Application.Activities.Models.Output;
using Application.Activities.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Common;

namespace API.Activities.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        public ActivitiesController(IMediator mediator, IMapper mapper)
            : base(mediator, mapper)
        { }

        [HttpGet]
        public async Task<IActionResult> GetActivities()
            => this.HandleMappingResult<IEnumerable<ActivityOutputModel>, IEnumerable<ActivityApiModel>>(
                await this.Mediator.Send(new List.Query()));
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivity(int id)
            =>  this.HandleMappingResult<ActivityOutputModel, ActivityApiModel>(
                    await this.Mediator.Send(new Details.Query(id)));
        
        [HttpDelete("{id}")]
        [Authorize(Policy = GlobalConstants.IsActivityHostPolicy)]
        public async Task<IActionResult> DeleteActivity(int id)
            => this.HandleResult(await this.Mediator.Send(new Delete.Command(id)));

        [HttpGet("categories")]
        public async Task<IActionResult> GetActivityCategories()
            => this.HandleMappingResult<IEnumerable<CategoryOutputModel>, IEnumerable<CategoryApiModel>>(
                await this.Mediator.Send(new Categories.Query()));
        
        [HttpPost("updateStatus/{id}")]
        [Authorize(Policy = GlobalConstants.IsActivityHostPolicy)]
        public async Task<IActionResult> UpdateStatus(int id)
            => this.HandleResult(await this.Mediator.Send(new UpdateStatus.Command(id)));

        [HttpPost("attend/{id}")]
        public async Task<IActionResult> Attend(int id)
            => this.HandleResult(await this.Mediator.Send(new Attend.Command(id, this.GetCurrentUserId())));

        [HttpPost]
        public async Task<IActionResult> CreateActivity(ActivityApiModel request)
            => await CreateEditActivity<CreateActivityInputModel, int>(request, inputModel => new Create.Command(inputModel));
        
        [HttpPut]
        [Authorize(Policy = GlobalConstants.IsActivityHostPolicy)]
        public async Task<IActionResult> EditActivity(ActivityApiModel request)
            => await CreateEditActivity<EditActivityInputModel, Unit>(request, inputModel => new Edit.Command(inputModel));
        
        private async Task<IActionResult> CreateEditActivity<TInputModel, TCommandResult>
            (ActivityApiModel request, Func<TInputModel, IRequest<Result<TCommandResult>>> commandCreator)
        where TInputModel : CreateEditActivityBaseInputModel
        {
            var inputModel = this.Mapper.Map<TInputModel>(request);

            inputModel.SetHostId(this.GetCurrentUserId());

            return this.HandleResult(await this.Mediator.Send(commandCreator.Invoke(inputModel)));
        }
    }
}