using System;

namespace API.Messages.Models;

public class SendMessageApiModel
{
    public string ReceiverUsername{ get; set; }
    
    public string Content { get; set; }
}