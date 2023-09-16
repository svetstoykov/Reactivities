using System.Threading.Tasks;
using Application.Messages.Models.Input;
using Reactivities.Common.Result.Models;

namespace Application.Messages.Interfaces;

public interface IMessagesClient
{
    Task<Result<bool>> SendMessageAsync(SendMessageInputModel message);
}