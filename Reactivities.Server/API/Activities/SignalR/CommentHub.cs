﻿using System.Threading.Tasks;
using Application.Activities.Commands;
using Application.Activities.Queries;
using Application.Common.Utility;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.Activities.SignalR;

public class CommentHub : Hub
{
    private readonly IMediator _mediator;
        
    public CommentHub(IMediator mediator)
    {
        this._mediator = mediator;
    }

    public async Task SendComment(AddComment.Command command)
    {
        var comment = await this._mediator.Send(command);

        await this.Clients.Group(command.ActivityId.ToString())
            .SendAsync("ReceiveComment", comment.Data);
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = this.Context.GetHttpContext();
        var activityId = httpContext!.Request.Query[GlobalConstants.ActivityIdQueryParam];
        await this.Groups.AddToGroupAsync(this.Context.ConnectionId, activityId);

        var commentsList = await this._mediator.Send(new GetComments.Query(int.Parse(activityId)));
        await this.Clients.Caller.SendAsync("LoadComments", commentsList.Data);
    }
}