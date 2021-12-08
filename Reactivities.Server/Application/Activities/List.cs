using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.Activities.Response;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<List<ActivityResponse>> { }

        public class Handler : BaseHandler<Query, List<ActivityResponse>>
        {
            public Handler(DataContext dataContext, IMapper mapper) 
                : base(dataContext, mapper)
            {}

            public override async Task<List<ActivityResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activities = await this.DataContext.Activities.ToListAsync();

                return this.Mapper.Map<List<ActivityResponse>>(activities);
            }

        }
    }
}
