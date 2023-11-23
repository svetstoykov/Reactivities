using Application.Common.Models.Pagination;
using Application.Messages.Interfaces;
using Application.Messages.Models.Input;
using Application.Messages.Models.Output;
using Infrastructure.Common.Extensions;
using Infrastructure.Common.Helpers;
using Infrastructure.Common.Settings;
using MassTransit;
using Microsoft.Extensions.Options;
using Reactivities.Common.Result.Models;

namespace Infrastructure.Messages.Services;

public class MessagesClient : IMessagesClient
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IRequestClient<GetSenderReceiverConversationRequestModel> _getConversationRequestClient;
    private readonly RabbitMqConfiguration _rabbitMqConfiguration;

    public MessagesClient(
        ISendEndpointProvider sendEndpointProvider,
        IRequestClient<GetSenderReceiverConversationRequestModel> getConversationRequestClient,
        IOptionsMonitor<RabbitMqConfiguration> rabbitMqConfiguration)
    {
        this._sendEndpointProvider = sendEndpointProvider;
        this._getConversationRequestClient = getConversationRequestClient;
        this._rabbitMqConfiguration = rabbitMqConfiguration.CurrentValue;
    }

    public async Task<PaginatedResult<MessageOutputModel>> GetMessagesConversationAsync(
        string senderUsername, string receiverUsername, int startIndex, int pageSize)
    {
        var request = new GetSenderReceiverConversationRequestModel
        {
            SenderUsername = senderUsername,
            PageSize = pageSize,
            ReceiverUsername = receiverUsername,
            PageIndex = startIndex
        };
        
        var response = await this._getConversationRequestClient
            .GetResponse<PaginatedResult<MessageOutputModel>>(request);

        return response.Message;
    }

    public async Task<Result<bool>> SendMessageAsync(SendMessageInputModel message)
    {
        var addressUri = this._rabbitMqConfiguration.MessagingExchangeName
            .ToExchangeAddressUri();
        
        var endpoint = await this._sendEndpointProvider.GetSendEndpoint(addressUri);

        await endpoint.Send(message);
        
        return Result<bool>.Success(true);
    }
}