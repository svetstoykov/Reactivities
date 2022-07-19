using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.Models.Output;
using Application.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<Result<IEnumerable<ActivityOutputModel>>>
        {
        }

        public class Handler : BaseHandler<Query, Result<IEnumerable<ActivityOutputModel>>>
        {
            public Handler(DataContext dataContext, IMapper mapper)
                : base(dataContext, mapper)
            {
            }

            public override async Task<Result<IEnumerable<ActivityOutputModel>>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var activities = await this.DataContext.Activities.ToListAsync(cancellationToken);

                var outputModels = this.Mapper.Map<IEnumerable<ActivityOutputModel>>(activities);

                return Result<IEnumerable<ActivityOutputModel>>.Success(outputModels);
            }
        }
    }
}