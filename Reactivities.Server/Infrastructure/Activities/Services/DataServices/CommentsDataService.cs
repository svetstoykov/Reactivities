using Application.Activities.Interfaces.DataServices;
using Application.Activities.Models.Output;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Activities;
using Microsoft.EntityFrameworkCore;
using Reactivities.Common.DataServices.Services;

namespace Infrastructure.Activities.Services.DataServices;

public class CommentsDataService : EntityDataService<DataContext,Comment>, ICommentsDataService
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