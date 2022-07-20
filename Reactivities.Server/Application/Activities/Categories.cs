using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.Models.Output;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Persistence;

namespace Application.Activities
{
    public class Categories
    {
        public class Query : IRequest<Result<IEnumerable<CategoryOutputModel>>>
        {
            
        }

        public class Handler : IRequestHandler<Query, Result<IEnumerable<CategoryOutputModel>>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;

            public Handler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;
            }

            public async Task<Result<IEnumerable<CategoryOutputModel>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var categories = await _dataContext.Categories.ToListAsync();

                var outputModels = _mapper.Map<IEnumerable<CategoryOutputModel>>(categories);

                return Result<IEnumerable<CategoryOutputModel>>.Success(outputModels);
            }
        }
    }
}
