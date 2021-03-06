using System.Threading;
using System.Threading.Tasks;
using Application.Activities.Models.Output;
using Application.Common;
using AutoMapper;
using MediatR;
using Models.Common;
using Persistence;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<Result<ActivityOutputModel>>
        {
            public Query(int id)
            {
                Id = id;
            }

            public int Id { get; set; }
        }

        public class Handler : BaseHandler<Query, Result<ActivityOutputModel>>
        {
            public Handler(DataContext dataContext, IMapper mapper)
                : base(dataContext, mapper)
            {
            }
    
            public override async Task<Result<ActivityOutputModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var domainDto = await this.DataContext.Activities.FindAsync(new object[] {request.Id}, cancellationToken);

                var outputModel = this.Mapper.Map<ActivityOutputModel>(domainDto);

                return Result<ActivityOutputModel>.Success(outputModel);
            }
        }
    }
}