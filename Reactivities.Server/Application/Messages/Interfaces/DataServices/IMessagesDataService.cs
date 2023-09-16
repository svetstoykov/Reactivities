using System.Threading.Tasks;
using Application.Common.Models.Pagination;
using Application.Messages.Models.Output;
using Domain.Messages;
using Reactivities.Common.DataServices.Abstractions.Interfaces;

namespace Application.Messages.Interfaces.DataServices;

public interface IMessagesDataService : IEntityDataService<Message>
{
    Task<PaginatedResult<MessageOutputModel>> GetMessagesConversationAsync(
        string senderUsername, string receiverUsername, int startIndex, int pageSize);
}