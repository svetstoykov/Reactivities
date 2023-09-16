using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Messages.Interfaces;
using Application.Messages.Models.Input;
using Application.Profiles.Interfaces;
using MediatR;
using Reactivities.Common.Result.Models;

namespace Application.Messages.Commands;

public class SendMessage
{
    public class Command : IRequest<Result<bool>>
    {
        public Command(string receiverUsername, string content)
        {
            this.ReceiverUsername = receiverUsername;
            this.Content = content;
            this.DateSent = DateTime.UtcNow;
        }
        public string ReceiverUsername{ get; }
        public string Content { get;}
        public DateTime DateSent { get; }
    }
    
    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly IProfileAccessor _profileAccessor;
        private readonly IMessagesClient _messagesClient;
        
        public Handler(
            IProfileAccessor profileAccessor,
            IMessagesClient messagesClient)
        {
            this._profileAccessor = profileAccessor;
            this._messagesClient = messagesClient;
        }

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var messageRequestModel = new SendMessageInputModel()
            {
                SenderUsername = this._profileAccessor.GetLoggedInUsername(),
                ReceiverUsername = request.ReceiverUsername,
                DateSent = DateTime.UtcNow,
                Content = request.Content
            };
            
            return await this._messagesClient.SendMessageAsync(messageRequestModel);
        }
    }
}