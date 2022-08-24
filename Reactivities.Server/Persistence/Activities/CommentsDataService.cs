using Application.Activities.DataServices;
using Domain.Activities;
using Persistence.Common.DataServices;

namespace Persistence.Activities
{
    public class CommentsDataService : EntityDataService<Comment>, ICommentsDataService
    {
        public CommentsDataService(DataContext dataContext) : base(dataContext)
        {
        }
    }
}