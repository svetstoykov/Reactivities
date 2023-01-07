using System.Threading.Tasks;
using Reactivities.Common.Messages.Models.Request;
using Reactivities.Common.Messages.Models.Response;
using Reactivities.Common.Result.Models;

namespace Application.Messages.Interfaces;

public interface IMessagesMqClient
{
    Task<Result<bool>> SendMessageAsync(SendMessageRequestModel request);

    Task<Result<SenderReceiverConversationResponseModel>> GetConversationAsync(GetSenderReceiverConversationRequestModel request);
}