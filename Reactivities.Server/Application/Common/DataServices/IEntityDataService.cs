using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common.Base;

namespace Application.Common.DataServices
{
    public interface IEntityDataService<TDomainEntity>
        where TDomainEntity : DomainEntity
    {
        public IQueryable<TDomainEntity> GetAsQueryable();

        public void Create(TDomainEntity activity);

        public void Update(TDomainEntity activity);

        public void Remove(TDomainEntity activity);

        Task SaveChangesAsync(CancellationToken token = default, string errorToShow = null);
    }
}
