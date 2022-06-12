using System.Threading;
using System.Threading.Tasks;
using Application.Activities.Models.Input;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest<int>
        {
            public Command(CreateActivityInputModel dto)
            {
                this.Dto = dto;
            }

            public CreateActivityInputModel Dto { get; set; }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;

            public Handler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var coreDto = this._mapper.Map<Activity>(request.Dto);

                this._dataContext.Activities.Add(coreDto);

                await this._dataContext.SaveChangesAsync();

                return coreDto.Id;
            }
        }
    }
}
