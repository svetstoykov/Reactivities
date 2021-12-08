using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Models.Activities.Request;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest
        {
            public Command(CreateActivityRequest dto)
            {
                this.Dto = dto;
            }

            public CreateActivityRequest Dto { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;

            public Handler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var coreDto = this._mapper.Map<Activity>(request.Dto);

                this._dataContext.Activities.Add(coreDto);

                await this._dataContext.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
