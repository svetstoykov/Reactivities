using System.Threading.Tasks;
using Reactivities.Common.Messages.Models.Request;

namespace Application.Messages.Interfaces;

public interface IMessagingServiceClient
{
    public Task<bool> SendMessageAsync(SendMessageRequestModel request);
}