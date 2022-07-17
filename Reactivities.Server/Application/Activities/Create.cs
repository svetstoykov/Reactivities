using System.Threading;
using System.Threading.Tasks;
using Application.Activities.Models.Input;
using AutoMapper;
using Domain;
using MediatR;
using Models.Common;
using Models.ErrorHandling.Helpers;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Command(CreateActivityInputModel dto)
            {
                this.Dto = dto;
            }

            public CreateActivityInputModel Dto { get; set; }
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
                var coreDto = this._mapper.Map<Activity>(request.Dto);

                this._dataContext.Activities.Add(coreDto);

                var entityChangeResult = await this._dataContext.SaveChangesAsync(cancellationToken);

                if (entityChangeResult <= 0)
                {
                    return Result<Unit>.Failure(ActivitiesErrorMessagesHelper.CreateError);
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}