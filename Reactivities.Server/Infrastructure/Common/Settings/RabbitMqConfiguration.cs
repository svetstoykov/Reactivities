namespace Infrastructure.Common.Settings;

public class RabbitMqConfiguration
{
    public string MessagingExchangeName { get; set; }
    
    public string GetConversationExchangeName { get; set; }
}