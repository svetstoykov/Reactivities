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
using Application.Common.Models;
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
        public async Task<IActionResult> GetActivities([FromQuery] int? pageSize, int? pageNumber)
            => this.HandleMappingResult<PaginatedResult<ActivityOutputModel>, PaginatedResult<ActivityApiModel>>(
                await this.Mediator.Send(new List.Query(pageSize ?? 5, pageNumber ?? 1)));
        

        [HttpGet($"{{{GlobalConstants.ActivityIdQueryParam}:int}}")]
        public async Task<IActionResult> GetActivity(int activityId)
            =>  this.HandleMappingResult<ActivityOutputModel, ActivityApiModel>(
                    await this.Mediator.Send(new Details.Query(activityId)));
        

        [HttpDelete($"{{{GlobalConstants.ActivityIdQueryParam}:int}}")]
        [Authorize(Policy = GlobalConstants.IsActivityHostPolicy)]
        public async Task<IActionResult> DeleteActivity(int activityId)
            => this.HandleResult(await this.Mediator.Send(new Delete.Command(activityId)));


        [HttpGet("categories")]
        public async Task<IActionResult> GetActivityCategories()
            => this.HandleMappingResult<IEnumerable<CategoryOutputModel>, IEnumerable<CategoryApiModel>>(
                await this.Mediator.Send(new Categories.Query()));
        

        [HttpPost($"updateStatus/{{{GlobalConstants.ActivityIdQueryParam}:int}}")]
        [Authorize(Policy = GlobalConstants.IsActivityHostPolicy)]
        public async Task<IActionResult> UpdateStatus(int activityId)
            => this.HandleResult(await this.Mediator.Send(new UpdateStatus.Command(activityId)));


        [HttpPost($"updateAttendance/{{{GlobalConstants.ActivityIdQueryParam}:int}}")]
        public async Task<IActionResult> UpdateAttendance(int activityId)
            => this.HandleResult(await this.Mediator.Send(new UpdateAttendance.Command(activityId)));


        [HttpPost]
        public async Task<IActionResult> CreateActivity(ActivityApiModel request)
            => await this.CreateEditActivity<CreateActivityInputModel, int>(
                request, inputModel => new Create.Command(inputModel));
        

        [HttpPut]
        [Authorize(Policy = GlobalConstants.IsActivityHostPolicy)]
        public async Task<IActionResult> EditActivity(ActivityApiModel request)
            => await this.CreateEditActivity<EditActivityInputModel, Unit>(
                request, inputModel => new Edit.Command(inputModel));
        

        private async Task<IActionResult> CreateEditActivity<TInputModel, TCommandResult>
            (ActivityApiModel request, Func<TInputModel, IRequest<Result<TCommandResult>>> commandCreator)
        where TInputModel : CreateEditActivityBaseInputModel
        {
            var inputModel = this.Mapper.Map<TInputModel>(request);

            inputModel.SetHostId(this.GetCurrentUserId);

            return this.HandleResult(await this.Mediator.Send(commandCreator.Invoke(inputModel)));
        }
    }
}