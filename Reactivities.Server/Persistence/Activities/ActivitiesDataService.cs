using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.DataServices;
using Application.Activities.ErrorHandling;
using Application.Activities.Models.Output;
using Application.Common.Models;
using Application.Common.Models.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Activities;
using Microsoft.EntityFrameworkCore;
using Persistence.Common.DataServices;
using Persistence.Common.Extensions;

namespace Persistence.Activities
{
    public class ActivitiesDataService : EntityDataService<Activity>, IActivitiesDataService
    {
        private readonly IMapper _mapper;

        public ActivitiesDataService(DataContext dataContext, IMapper mapper) : base(dataContext)
        {
            this._mapper = mapper;
        }

        public async Task<PaginatedResult<ActivityOutputModel>> GetPaginatedActivitiesAsync
            (IQueryable<Activity> activitiesQueryable, int pageSize, int pageNumber, string loggedInUsername, CancellationToken cancellationToken = default)
            => await activitiesQueryable
                .ProjectTo<ActivityOutputModel>(this._mapper.ConfigurationProvider, 
                    new { currentProfile = loggedInUsername })
                .PaginateAsync(pageSize, pageNumber, cancellationToken);

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