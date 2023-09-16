namespace Application.Messages.Models.Input;

public class GetSenderReceiverConversationRequestModel
{
    public string SenderUsername { get; init; }

    public string ReceiverUsername { get; init; }
    
    public int PageIndex { get; init; }
        
    public int PageSize { get; init; }
}