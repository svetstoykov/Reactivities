using Application.Common.Models.Pagination;
using Application.Messages.Interfaces.DataServices;
using Application.Messages.Models.Output;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Messages;
using Infrastructure.Common.Extensions;
using Reactivities.Common.DataServices.Services;

namespace Infrastructure.Messages.Services.DataServices;

public class MessagesDataService : EntityDataService<DataContext, Message>, IMessagesDataService
{
    private readonly IMapper _mapper;

    public MessagesDataService(
        DataContext dataContext,
        IMapper mapper) : base(dataContext)
    {
        this._mapper = mapper;
    }

    public async Task<PaginatedResult<MessageOutputModel>> GetMessagesConversationAsync(
        string senderUsername, string receiverUsername, int startIndex, int pageSize)
    {
        var messagesQuery = this.DataSet
            .Where(m => m.Sender.UserName == senderUsername
                        && m.Receiver.UserName == receiverUsername)
            .OrderByDescending(m => m.DateSent)
            .ProjectTo<MessageOutputModel>(this._mapper.ConfigurationProvider);

        return await messagesQuery.PaginateAsync(pageSize, startIndex);
    }
}