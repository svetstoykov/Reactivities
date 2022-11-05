using System.Collections.Generic;
using System.Threading.Tasks;
using API.Activities.Models.Comments;
using API.Common.Controllers;
using Application.Activities.Commands;
using Application.Activities.Models.Output;
using Application.Activities.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Activities.Controllers;

public class CommentsController : BaseApiController
{
    public CommentsController(IMediator mediator, IMapper mapper)
        : base(mediator, mapper)
    {
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(AddCommentApiModel request)
        => this.HandleMappingResult<CommentOutputModel, CommentApiModel>(
            await this.Mediator.Send(new AddComment.Command(request.Content, request.ActivityId, request.Username)));

    [HttpGet]
    public async Task<IActionResult> GetComments(int activityId)
        => this.HandleMappingResult<IEnumerable<CommentOutputModel>, IEnumerable<CommentApiModel>>(
            await this.Mediator.Send(new GetComments.Query(activityId)));
}