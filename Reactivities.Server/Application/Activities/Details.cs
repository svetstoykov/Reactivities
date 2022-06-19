using System.Threading;
using System.Threading.Tasks;
using Application.Activities.Models.Output;
using Application.Common;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<ActivityOutputModel>
        {
            public Query(int id)
            {
                Id = id;
            }

            public int Id { get; set; }
        }

        public class Handler : BaseHandler<Query, ActivityOutputModel>
        {
            public Handler(DataContext dataContext, IMapper mapper)
                : base(dataContext, mapper)
            { }

            public override async Task<ActivityOutputModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await this.DataContext.Activities.FindAsync(request.Id, cancellationToken);

                return this.Mapper.Map<ActivityOutputModel>(activity);
            }

        }
    }
}
