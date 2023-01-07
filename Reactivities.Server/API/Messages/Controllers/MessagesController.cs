using System.Threading.Tasks;
using API.Common.Controllers;
using API.Messages.Models;
using Application.Messages.Commands;
using Application.Messages.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Messages.Controllers;

public class MessagesController : BaseApiController
{
    public MessagesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost("sendMessage")]
    public async Task<IActionResult> SendMessage(SendMessageApiModel request)
        => this.HandleResult(await this.Mediator.Send(
            new SendMessage.Command(request.SenderUsername, request.ReceiverUsername, request.Content)));

    [HttpGet]
    public async Task<IActionResult> GetConversation([FromQuery] GetConversationApiModel request)
        => this.HandleResult(await this.Mediator.Send(new GetConversation.Query(
            request.SenderUsername,
            request.ReceiverUsername,
            request.InitialMessagesLoadCount,
            request.DateFrom,
            request.DateTo)));
}