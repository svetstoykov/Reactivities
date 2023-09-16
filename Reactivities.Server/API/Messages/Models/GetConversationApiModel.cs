using System;
using Application.Common.Models.Pagination;

namespace API.Messages.Models;

public class GetConversationApiModel : PagingParams
{
    public string SenderUsername { get; set; }

    public string ReceiverUsername { get; set; }

    public int InitialMessagesLoadCount { get; set;}
}