using System.Threading.Tasks;
using Application.Common.Models.Pagination;
using Application.Messages.Models.Input;
using Application.Messages.Models.Output;
using Reactivities.Common.Result.Models;

namespace Application.Messages.Interfaces;

public interface IMessagesClient
{
    Task<PaginatedResult<MessageOutputModel>> GetMessagesConversationAsync(
        string senderUsername, string receiverUsername, int startIndex, int pageSize);
    
    Task<Result<bool>> SendMessageAsync(SendMessageInputModel message);
}