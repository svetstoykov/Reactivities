namespace Infrastructure.Common.Helpers;

public static class UriHelper
{
    public static Uri GetQueueAddress(string queueName) => new($"queue:{queueName}");
}