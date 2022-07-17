using System.Threading;
using System.Threading.Tasks;
using Application.Activities.Models.Input;
using AutoMapper;
using MediatR;
using Models.Common;
using Models.ErrorHandling.Helpers;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Command(EditActivityInputModel dto)
            {
                this.Dto = dto;
            }

            public EditActivityInputModel Dto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;

            public Handler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activityCore = await this._dataContext.Activities.FindAsync(request.Dto.Id);

                if (activityCore == null)
                {
                    return Result<Unit>.Failure(
                        string.Format(ActivitiesErrorMessagesHelper.DoesNotExist, request.Dto.Id));
                }

                this._mapper.Map(request.Dto, activityCore);

                this._dataContext.Activities.Update(activityCore);

                await this._dataContext.SaveChangesAsync(cancellationToken);

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
