using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Messages.Interfaces;
using AutoMapper;
using MediatR;
using Reactivities.Common.Messages.Models.Request;
using Reactivities.Common.Result.Models;

namespace Application.Messages.Commands;

public class SendMessage
{
    public class Command : IRequest<Result<bool>>
    {
        public Command(string senderUsername, string receiverUsername, string content)
        {
            this.SenderUsername = senderUsername;
            this.ReceiverUsername = receiverUsername;
            this.Content = content;
            this.DateSent = DateTime.UtcNow;
        }
        
        public string SenderUsername { get; }
        public string ReceiverUsername{ get; }
        public string Content { get;}
        public DateTime DateSent { get; }
    }
    
    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly IMessagingServiceClient _messagingServiceClient;
        private readonly IMapper _mapper;
        
        public Handler(IMessagingServiceClient messagingServiceClient, IMapper mapper)
        {
            this._messagingServiceClient = messagingServiceClient;
            this._mapper = mapper;
        }

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var messageRequestModel = this._mapper.Map<SendMessageRequestModel>(request);

            return await this._messagingServiceClient.SendMessageAsync(messageRequestModel);
        }
    }
}