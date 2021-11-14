using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Command(int id)
            {
                this.Id = id;
            }

            public int Id { get; set; }
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
                var coreDto = await this._dataContext.Activities.FindAsync(request.Id);

                if (coreDto != null)
                {
                    this._dataContext.Remove(coreDto);

                    await this._dataContext.SaveChangesAsync();
                }

                return Unit.Value;
            }
        }
    }
}
