using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        private readonly DataContext _dataContext;

        public ActivitiesController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return await this._dataContext.Activities.ToListAsync();
        }

        [HttpGet("{id}")]
        public  async  Task<ActionResult<Activity>> GetActivity(int id)
        {
            return await this._dataContext.Activities.FindAsync(id);
        }

    }
}
