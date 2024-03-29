﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Activities.Models;
using Application.Activities.Interfaces.DataServices;
using Application.Common.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Identity.Policies;

public class IsHostRequirement : IAuthorizationRequirement
{
}

public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
{
    private readonly IActivitiesDataService _activitiesDataService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IsHostRequirementHandler(IActivitiesDataService activitiesDataService, IHttpContextAccessor httpContextAccessor)
    {
        this._activitiesDataService = activitiesDataService;
        this._httpContextAccessor = httpContextAccessor;
    }
        
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
        if (userId == null) return;

        var activityId = await this.GetActivityIdFromRequestAsync();

        var activity = await this._activitiesDataService
            .GetAsQueryable()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == activityId);

        if (activity == null) return;
            
        if(activity.HostId == Convert.ToInt32(userId)) context.Succeed(requirement);
    }

    private async Task<int> GetActivityIdFromRequestAsync()
    {
        var request = this._httpContextAccessor.HttpContext?.Request;

        if (request == null) return default;
            
        var idFromQuery = int.Parse(request.RouteValues
            .SingleOrDefault(v => v.Key == GlobalConstants.ActivityIdQueryParam).Value?.ToString() ?? default(int).ToString());

        if (idFromQuery != default) return idFromQuery;

        var requestContent = await GetRequestBodyContent(request);

        var activityApiModel = JsonConvert.DeserializeObject<ActivityApiModel>(requestContent);

        return activityApiModel?.Id ?? default;
    }

    private static async Task<string> GetRequestBodyContent(HttpRequest request)
    {
        request.EnableBuffering();
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        await request.Body.ReadAsync(buffer, 0, buffer.Length);

        var requestContent = Encoding.UTF8.GetString(buffer);

        request.Body.Position = 0;
        return requestContent;
    }
}