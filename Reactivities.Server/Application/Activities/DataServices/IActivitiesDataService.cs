using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DataServices;
using Domain;

namespace Application.Activities.DataServices
{
    public interface IActivitiesDataService : IBaseDataService
    {
        Task<IList<Activity>> GetActivitiesAsync(Func<Activity, bool> predicate = null);

        Task<IList<Category>> GetCategoriesAsync(Func<Category, bool> predicate = null);

        Task<Activity> GetByIdAsync(int id);

        public void Create(Activity activity);

        public void Update(Activity activity);

        public void Remove(Activity activity);

        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
