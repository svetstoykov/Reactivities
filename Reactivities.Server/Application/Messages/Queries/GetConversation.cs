using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models.Pagination;
using Application.Messages.Interfaces;
using Application.Messages.Models.Input;
using Application.Messages.Models.Output;
using Application.Profiles.Interfaces;
using MediatR;
using Reactivities.Common.Result.Models;

namespace Application.Messages.Queries;

public class GetConversation
{
    public class Query : IRequest<Result<PaginatedResult<MessageOutputModel>>>
    {
        public Query(string receiverUsername, int pageNumber, int pageSize)
        {
            this.ReceiverUsername = receiverUsername;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }
        
        public string ReceiverUsername { get; }
        
        public int PageNumber { get; set; }
        
        public int PageSize { get; set; }
    }
    
    public class Handler : IRequestHandler<Query, Result<PaginatedResult<MessageOutputModel>>>
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

        public async Task<Result<PaginatedResult<MessageOutputModel>>> Handle(
            Query request, CancellationToken cancellationToken)
        {
            var requestModel = new GetSenderReceiverConversationRequestModel
            {
                SenderUsername = this._profileAccessor.GetLoggedInUsername(),
                ReceiverUsername = request.ReceiverUsername,
                PageSize = request.PageSize,
                PageIndex = request.PageNumber - 1
            };
            
            var paginatedResult = await this._messagesClient.GetMessagesConversationAsync(
                requestModel.SenderUsername, request.ReceiverUsername, request.PageNumber, request.PageSize);
            
            return Result<PaginatedResult<MessageOutputModel>>
                .Success(paginatedResult);
        }
    }
}