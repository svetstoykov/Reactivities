using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Messages.Models;
using AutoMapper;
using EasyNetQ;
using MediatR;
using Models.Common;

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
        private readonly IMapper _mapper;
        private readonly IBus _messageBus;

        public Handler(IMapper mapper, IBus messageBus)
        {
            this._mapper = mapper;
            this._messageBus = messageBus;
        }

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var sendMessageInputModel = this._mapper.Map<SendMessageInputModel>(request);

            // TODO implement usage for message bus
            await Task.Run(() => 5);
            
            return Result<bool>.Success(true);
        }
    }
}