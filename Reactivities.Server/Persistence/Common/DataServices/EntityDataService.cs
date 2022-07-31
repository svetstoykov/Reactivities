using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.ErrorHandling;
using Application.Common.DataServices;
using Domain.Common.Base;
using Microsoft.EntityFrameworkCore;
using Models.ErrorHandling;

namespace Persistence.Common.DataServices
{
    public class EntityDataService<TDomainEntity> : IEntityDataService<TDomainEntity>
        where TDomainEntity : DomainEntity
    {
        protected readonly DataContext DataContext;
        protected readonly DbSet<TDomainEntity> DataSet;

        public EntityDataService(DataContext dataContext)
        {
            this.DataContext = dataContext;
            this.DataSet = dataContext.Set<TDomainEntity>();
        }

        public IQueryable<TDomainEntity> GetAsQueryable()
            => this.DataSet;

        public async Task SaveChangesAsync(CancellationToken token = default, string errorToShow = null)
        {
            if (await this.DataContext.SaveChangesAsync(token) > 0)
                return;

            throw new AppException(errorToShow ?? ActivitiesErrorMessages.UnableToSaveChanges);
        }

        public void Create(TDomainEntity entity)
            => this.DataSet.Add(entity);

        public void Update(TDomainEntity entity)
            => this.DataSet.Update(entity);

        public void Remove(TDomainEntity entity)
            => this.DataSet.Remove(entity);
    }
}
