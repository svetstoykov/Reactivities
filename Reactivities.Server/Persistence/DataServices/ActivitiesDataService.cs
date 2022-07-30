using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Domain.Activities;
using Microsoft.EntityFrameworkCore;
using Models.ErrorHandling;
using Models.ErrorHandling.Helpers;

namespace Persistence.DataServices
{
    public class ActivitiesDataService : IActivitiesDataService
    {
        private readonly DataContext _dataContext;

        public ActivitiesDataService(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public IQueryable<Activity> GetActivitiesQueryable()
            => this._dataContext.Activities;

        public async Task<ICollection<Category>> GetCategoriesAsync(Func<Category, bool> predicate = null)
            => await this._dataContext.Categories
                .Where(a => predicate == null || predicate(a))
                .ToListAsync();

        public async Task<Activity> GetByIdAsync(int id, bool throwExceptionIfNull = true)
        {
            var activity = await this._dataContext.Activities
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

        public void Create(Activity activity)
            => this._dataContext.Activities.Add(activity);

        public void Update(Activity activity)
            => this._dataContext.Activities.Update(activity);

        public void Remove(Activity activity)
            => this._dataContext.Activities.Remove(activity);

        public async Task SaveChangesAsync(CancellationToken token = default, string errorToShow = null)
        {
            if (await this._dataContext.SaveChangesAsync(token) > 0)
                return;

            throw new AppException(errorToShow ?? ActivitiesErrorMessages.UnableToSaveChanges);
        }
    }
}