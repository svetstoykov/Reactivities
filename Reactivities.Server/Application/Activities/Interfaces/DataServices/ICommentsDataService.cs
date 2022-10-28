using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.Models.Output;
using Application.Common.Interfaces.DataServices;
using Domain.Activities;

namespace Application.Activities.Interfaces.DataServices;

public interface ICommentsDataService : IEntityDataService<Comment>
{
    Task<IEnumerable<CommentOutputModel>> GetActivitiesCommentsAsync(
        int activityId, CancellationToken cancellationToken);
}