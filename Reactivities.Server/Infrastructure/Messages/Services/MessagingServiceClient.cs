using Application.Messages.Interfaces;
using EasyNetQ;
using Reactivities.Common.Messages.Models.Request;

namespace Infrastructure.Messages.Services;

public class MessagingServiceClient : IMessagingServiceClient
{
    private readonly IBus _messageBus;

    public MessagingServiceClient(IBus messageBus)
    {
        this._messageBus = messageBus;
    }

    public async Task<bool> SendMessageAsync(SendMessageRequestModel request)
        => await this._messageBus.Rpc.RequestAsync<SendMessageRequestModel, bool>(request);
}