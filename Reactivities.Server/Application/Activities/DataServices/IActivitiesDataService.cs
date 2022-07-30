using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.DataServices;
using Domain.Activities;

namespace Application.Activities.DataServices
{
    public interface IActivitiesDataService : IEntityDataService<Activity>
    {
        Task<ICollection<Category>> GetCategoriesAsync(Func<Category, bool> predicate = null);

        Task<Activity> GetByIdAsync(int id, bool throwExceptionIfNull = true);
    }
}