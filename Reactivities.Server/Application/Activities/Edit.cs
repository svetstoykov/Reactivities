using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Models.Activities;
using Models.Activities.Request;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Command(EditActivityRequest dto)
            {
                this.Dto = dto;
            }

            public EditActivityRequest Dto { get; set; }
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
                var activityCore = await this._dataContext.Activities.FindAsync(request.Dto.Id);

                if (activityCore != null)
                {
                    this._mapper.Map(request.Dto, activityCore);

                    this._dataContext.Activities.Update(activityCore);
                }

                await this._dataContext.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
