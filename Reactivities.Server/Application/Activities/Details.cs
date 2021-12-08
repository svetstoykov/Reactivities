using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using AutoMapper;
using Domain;
using MediatR;
using Models.Activities.Response;
using Persistence;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<ActivityResponse>
        {
            public Query(int id )
            {
                Id = id;
            }

            public int Id { get; set; }
        }

        public class Handler : BaseHandler<Query, ActivityResponse>
        {
            public Handler(DataContext dataContext, IMapper mapper) 
                : base(dataContext, mapper)
            { }

            public override async Task<ActivityResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await this.DataContext.Activities.FindAsync(request.Id);

                return this.Mapper.Map<ActivityResponse>(activity);
            }

        }
    }
}
