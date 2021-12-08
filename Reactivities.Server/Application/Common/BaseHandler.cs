using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Common
{
    public abstract class BaseHandler<TQuery, TEntity> : IRequestHandler<TQuery, TEntity>
        where TQuery: IRequest<TEntity>
    {
        protected readonly DataContext DataContext;
        protected readonly IMapper Mapper;

        protected BaseHandler(DataContext dataContext, IMapper mapper)
        {
            this.DataContext = dataContext;
            this.Mapper = mapper;
        }

        public abstract Task<TEntity> Handle(TQuery request, CancellationToken cancellationToken);
    }
}
