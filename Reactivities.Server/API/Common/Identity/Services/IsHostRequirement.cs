using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using API.Activities.Models;
using Application.Activities.DataServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Identity.Services
{
    public class IsHostRequirement : IAuthorizationRequirement
    {
    }

    public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
    {
        private readonly IActivitiesDataService _activitiesDataService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IsHostRequirementHandler(IActivitiesDataService activitiesDataService, IHttpContextAccessor httpContextAccessor)
        {
            _activitiesDataService = activitiesDataService;
            _httpContextAccessor = httpContextAccessor;
        }
        
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (userId == null) return;

            var activityId = await GetActivityIdFromRequestAsync();

            var activity = await _activitiesDataService
                .GetActivitiesQueryable()
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == activityId);

            if (activity == null) return;
            
            if(activity.HostId == userId) context.Succeed(requirement);
        }

        private async Task<int> GetActivityIdFromRequestAsync()
        {
            var request = _httpContextAccessor.HttpContext?.Request;

            if (request == null) return default;
            
            var idFromQuery = int.Parse(request.RouteValues
                .SingleOrDefault(v => v.Key == "id").Value?.ToString() ?? default(int).ToString());

            if (idFromQuery != default) return idFromQuery;

            using var content = new StreamContent(request.Body);
            
            var contentString = await content.ReadAsStringAsync();

            var activityApiModel = JsonSerializer.Deserialize<ActivityApiModel>(contentString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return activityApiModel?.Id ?? default;
        }
    }
}