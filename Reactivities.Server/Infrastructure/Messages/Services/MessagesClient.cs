using Application.Messages.Interfaces;
using Application.Messages.Models.Input;
using Infrastructure.Common.Helpers;
using Infrastructure.Common.Settings;
using MassTransit;
using Microsoft.Extensions.Options;
using Reactivities.Common.Result.Models;

namespace Infrastructure.Messages.Services;

public class MessagesClient : IMessagesClient
{
    private readonly IBusControl _busControl;
    private readonly RabbitMqConfiguration _rabbitMqConfiguration;

    public MessagesClient(
        IBusControl busControl,
        IOptionsMonitor<RabbitMqConfiguration> rabbitMqConfiguration)
    {
        this._busControl = busControl;
        this._rabbitMqConfiguration = rabbitMqConfiguration.CurrentValue;
    }

    public async Task<Result<bool>> SendMessageAsync(SendMessageInputModel message)
    {
        var queueAddress = UriHelper.GetQueueAddress(
            this._rabbitMqConfiguration.SendMessageQueueName);

        var sendEndpoint = await this._busControl.GetSendEndpoint(queueAddress);

        await sendEndpoint.Send(message);
        
        return Result<bool>.Success(true);
    }
}