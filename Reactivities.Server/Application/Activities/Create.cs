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
        public class Command : IRequest<Result<int>>
        {
            public Command(CreateActivityInputModel dto)
            {
                this.Dto = dto;
            }

            public CreateActivityInputModel Dto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<int>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;

            public Handler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;
            }

            public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
            {
                var domainDto = this._mapper.Map<Activity>(request.Dto);

                this._dataContext.Activities.Add(domainDto);

                var entityChangeResult = await this._dataContext.SaveChangesAsync(cancellationToken);

                return entityChangeResult <= 0 
                    ? Result<int>.Failure(ActivitiesErrorMessages.CreateError) 
                    : Result<int>.Success(domainDto.Id);
            }
        }
    }
}