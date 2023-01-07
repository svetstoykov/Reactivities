using System;

namespace API.Messages.Models;

public class GetConversationApiModel
{
    public string SenderUsername { get; }

    public string ReceiverUsername { get; }

    public int InitialMessagesLoadCount { get; }

    public DateTime? DateFrom { get; }

    public DateTime? DateTo { get; }
}