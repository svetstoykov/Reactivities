using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Application.Activities.ErrorHandling;
using Domain.Activities;
using Microsoft.EntityFrameworkCore;
using Persistence.Common.DataServices;

namespace Persistence.Activities
{
    public class ActivitiesDataService : EntityDataService<Activity>, IActivitiesDataService
    {
        public ActivitiesDataService(DataContext dataContext) : base(dataContext)
        {
        }
        
        public async Task<ICollection<Category>> GetCategoriesAsync(Func<Category, bool> predicate = null)
            => await this.DataContext.Categories
                .Where(a => predicate == null || predicate(a))
                .ToListAsync();

        public async Task<Activity> GetByIdAsync(int id, bool throwExceptionIfNull = true)
        {
            var activity = await this.DataSet
                .Include(a => a.Attendees)
                .Include(a => a.Host)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (throwExceptionIfNull && activity == null)
            {
                throw new KeyNotFoundException(string.Format(
                    ActivitiesErrorMessages.DoesNotExist, id));
            }

            return activity;
        }
    }
}