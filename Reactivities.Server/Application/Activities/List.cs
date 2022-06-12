using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.Models.Output;
using Application.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<List<ActivityOutputModel>> { }

        public class Handler : BaseHandler<Query, List<ActivityOutputModel>>
        {
            public Handler(DataContext dataContext, IMapper mapper) 
                : base(dataContext, mapper)
            {}

            public override async Task<List<ActivityOutputModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activities = await this.DataContext.Activities.ToListAsync();

                return this.Mapper.Map<List<ActivityOutputModel>>(activities);
            }

        }
    }
}
