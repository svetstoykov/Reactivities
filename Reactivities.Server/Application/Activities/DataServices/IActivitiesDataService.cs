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

        Task<Activity> GetByIdAsync(int id, bool throwExceptionIfNull = true);

        public void Create(Activity activity);

        public void Update(Activity activity);

        public void Remove(Activity activity);

        Task SaveChangesAsync(CancellationToken token = default, string errorToShow = null);
    }
}