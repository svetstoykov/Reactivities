using Application.Activities.DataServices;
using Application.Activities.Models.Output;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Activities;
using Infrastructure.Common.DataServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Activities
{
    public class CommentsDataService : EntityDataService<Comment>, ICommentsDataService
    {
        private readonly IMapper _mapper;

        public CommentsDataService(DataContext dataContext, IMapper mapper) : base(dataContext)
        {
            this._mapper = mapper;
        }

        public async Task<IEnumerable<CommentOutputModel>> GetActivitiesCommentsAsync(
            int activityId, CancellationToken cancellationToken)
            => await this.DataSet
                .Where(c => c.ActivityId == activityId)
                .OrderByDescending(c => c.CreatedAt)
                .ProjectTo<CommentOutputModel>(this._mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
    }
}