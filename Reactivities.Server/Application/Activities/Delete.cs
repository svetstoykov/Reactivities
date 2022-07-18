using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Models.Common;
using Models.ErrorHandling.Helpers;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Command(int id)
            {
                this.Id = id;
            }

            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _dataContext;

            public Handler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var domainDto = await this._dataContext.Activities.FindAsync(request.Id);

                if (domainDto == null)
                {
                    return Result<Unit>.NotFound(
                        ActivitiesErrorMessages.DoesNotExist);
                }

                this._dataContext.Remove(domainDto);

                await this._dataContext.SaveChangesAsync(cancellationToken);

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}