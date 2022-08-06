using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Application.Activities.Models.Output;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.Common;

namespace Application.Activities.Queries
{
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
            private readonly IMapper _mapper;

            public Handler(ICommentsDataService commentsDataService, IMapper mapper)
            {
                this._commentsDataService = commentsDataService;
                this._mapper = mapper;
            }

            public async Task<Result<IEnumerable<CommentOutputModel>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var comments = await this._commentsDataService
                    .GetAsQueryable()
                    .Where(c => c.ActivityId == request.ActivityId)
                    .ProjectTo<CommentOutputModel>(this._mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return Result<IEnumerable<CommentOutputModel>>.Success(comments);
            }
        }
    }
}