using Application.Messages.Interfaces;
using EasyNetQ;
using Reactivities.Common.Messages.Models.Request;
using Reactivities.Common.Messages.Models.Response;
using Reactivities.Common.Result.Models;

namespace Infrastructure.Messages.Services;

public class MessagesMqClient : IMessagesMqClient
{
    private readonly IBus _messageBus;

    public MessagesMqClient(IBus messageBus)
    {
        this._messageBus = messageBus;
    }

    public async Task<Result<bool>> SendMessageAsync(SendMessageRequestModel request)
        => await this._messageBus.Rpc
            .RequestAsync<SendMessageRequestModel, Result<bool>>(request);

    public async Task<Result<SenderReceiverConversationResponseModel>> GetConversationAsync(
        GetSenderReceiverConversationRequestModel request)
        => await this._messageBus.Rpc
            .RequestAsync<GetSenderReceiverConversationRequestModel, Result<SenderReceiverConversationResponseModel>>(request);
}