using System.Threading.Tasks;
using Reactivities.Common.Messages.Models.Request;
using Reactivities.Common.Result.Models;

namespace Application.Messages.Interfaces;

public interface IMessagingServiceClient
{
    public Task<Result<bool>> SendMessageAsync(SendMessageRequestModel request);
}