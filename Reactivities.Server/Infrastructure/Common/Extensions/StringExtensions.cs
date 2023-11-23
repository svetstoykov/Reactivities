namespace Infrastructure.Common.Extensions;

public static class StringExtensions
{
    public static Uri ToExchangeAddressUri(this string exchangeName) 
        => new($"exchange:{exchangeName}");
}