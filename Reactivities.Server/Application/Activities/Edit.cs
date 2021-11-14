using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Command(Activity activity)
            {
                Activity = activity;
            }
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _dataContext;

            public Handler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activityCore = await this._dataContext.Activities.FindAsync(request.Activity.Id);

                if (activityCore != null)
                {
                    activityCore.Title = request.Activity.Title;

                    this._dataContext.Activities.Update(activityCore);
                }

                await this._dataContext.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
