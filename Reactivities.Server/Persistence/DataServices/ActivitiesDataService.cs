﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DataServices
{
    public class ActivitiesDataService : IActivitiesDataService
    {
        private readonly DataContext _dataContext;

        public ActivitiesDataService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IList<Activity>> GetActivitiesAsync(Func<Activity, bool> predicate = null) 
            => await _dataContext.Activities
                .Where(a => predicate == null || predicate(a))
                .ToListAsync();

        public async Task<IList<Category>> GetCategoriesAsync(Func<Category, bool> predicate = null)
            => await _dataContext.Categories
                .Where(a => predicate == null || predicate(a))
                .ToListAsync();

        public async Task<Activity> GetByIdAsync(int id) 
            => await _dataContext.Activities.FindAsync(id);

        public void Create(Activity activity) 
            => _dataContext.Activities.Add(activity);

        public void Update(Activity activity) 
            => this._dataContext.Activities.Update(activity);

        public void Remove(Activity activity)
            => this._dataContext.Activities.Remove(activity);

        public async Task<int> SaveChangesAsync(CancellationToken token = default)
            => await _dataContext.SaveChangesAsync(token);
    }
}
