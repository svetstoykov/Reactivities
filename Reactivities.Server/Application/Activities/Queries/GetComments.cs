using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.Interfaces;
using Application.Activities.Interfaces.DataServices;
using Application.Activities.Models.Output;
using MediatR;
using Models.Common;

namespace Application.Activities.Queries;

public class GetComments
{
    public class Query : IRequest<Result<IEnumerable<CommentOutputModel>>>
    {
        public Query(int activityId)
        {
            this.ActivityId = activityId;
        }
            
        public int ActivityId { get; }
    }

    public class Handler : IRequestHandler<Query, Result<IEnumerable<CommentOutputModel>>>
    {
        private readonly ICommentsDataService _commentsDataService;

        public Handler(ICommentsDataService commentsDataService)
        {
            this._commentsDataService = commentsDataService;
        }

        public async Task<Result<IEnumerable<CommentOutputModel>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var comments = await this._commentsDataService.GetActivitiesCommentsAsync
                (request.ActivityId, cancellationToken);

            return Result<IEnumerable<CommentOutputModel>>.Success(comments);
        }
    }
}