namespace Application.Messages.Models.Output;

public class MessageOutputModel
{
    public string Content { get; set; }
    
    public string DateSent { get; set; }
    
    public string SenderUsername { get; set; }
    
    public string ReceiverUsername { get; set; }
}