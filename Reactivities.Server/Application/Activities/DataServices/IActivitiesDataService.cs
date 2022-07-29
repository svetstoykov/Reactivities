using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DataServices;
using Domain.Activities;

namespace Application.Activities.DataServices
{
    public interface IActivitiesDataService : IBaseDataService
    {
        public IQueryable<Activity> GetActivitiesQueryable();

        Task<ICollection<Category>> GetCategoriesAsync(Func<Category, bool> predicate = null);

        Task<Activity> GetByIdAsync(int id);
 
        public void Create(Activity activity);

        public void Update(Activity activity);

        public void Remove(Activity activity);

        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
