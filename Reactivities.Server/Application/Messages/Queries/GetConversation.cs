using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Messages.Interfaces;
using AutoMapper;
using MediatR;
using Reactivities.Common.Messages.Models.Request;
using Reactivities.Common.Messages.Models.Response;
using Reactivities.Common.Result.Models;

namespace Application.Messages.Queries;

public class GetConversation
{
    public class Query : IRequest<Result<SenderReceiverConversationResponseModel>>
    {
        public Query(string senderUsername, string receiverUsername, int initialMessagesLoadCount, DateTime? dateFrom, DateTime? dateTo)
        {
            this.SenderUsername = senderUsername;
            this.ReceiverUsername = receiverUsername;
            this.InitialMessagesLoadCount = initialMessagesLoadCount;
            this.DateFrom = dateFrom;
            this.DateTo = dateTo;
        }
        
        public string SenderUsername { get; }

        public string ReceiverUsername { get; }

        public int InitialMessagesLoadCount { get; }

        public DateTime? DateFrom { get; }

        public DateTime? DateTo { get; }
    }
    
    public class Handler : IRequestHandler<Query, Result<SenderReceiverConversationResponseModel>>
    {
        private readonly IMessagesMqClient _messagesMqClient;
        private readonly IMapper _mapper;

        public Handler(IMessagesMqClient messagesMqClient, IMapper mapper)
        {
            this._messagesMqClient = messagesMqClient;
            this._mapper = mapper;
        }

        public async Task<Result<SenderReceiverConversationResponseModel>> Handle(
            Query request, CancellationToken cancellationToken)
        {
            var requestModel = this._mapper.Map<GetSenderReceiverConversationRequestModel>(request);

            return await this._messagesMqClient.GetConversationAsync(requestModel);
        }
    }
}