using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Domain.Activities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Activity> GetByIdAsync(int id) 
            => await this._dataContext.Activities
                .Include(a => a.Attendees)
                .Include(a => a.Host)
                .FirstOrDefaultAsync(a => a.Id == id);

        public void Create(Activity activity) 
            => this._dataContext.Activities.Add(activity);

        public void Update(Activity activity) 
            => this._dataContext.Activities.Update(activity);

        public void Remove(Activity activity)
            => this._dataContext.Activities.Remove(activity);

        public async Task<int> SaveChangesAsync(CancellationToken token = default)
            => await this._dataContext.SaveChangesAsync(token);
    }
}
