using System;

namespace Application.Messages.Models.Input;

public class SendMessageInputModel
{
    public string SenderUsername { get; init; }

    public string ReceiverUsername { get; init; }

    public string Content { get; init; }

    public DateTime DateSent { get; init; }
}