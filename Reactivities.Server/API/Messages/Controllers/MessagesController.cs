using System.Threading.Tasks;
using API.Common.Controllers;
using API.Messages.Models;
using Application.Messages.Commands;
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
}