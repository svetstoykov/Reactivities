﻿using System.Threading;
using System.Threading.Tasks;
using Application.Activities.Interfaces.DataServices;
using MediatR;
using Reactivities.Common.Result.Models;

namespace Application.Activities.Commands;

public class UpdateStatus
{
    public class Command : IRequest<Result<Unit>>
    {
        public Command(int activityId)
        {
            this.ActivityId = activityId;
        }

        public int ActivityId { get; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IActivitiesDataService _activitiesDataService;

        public Handler(IActivitiesDataService activitiesDataService)
        {
            this._activitiesDataService = activitiesDataService;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await this._activitiesDataService
                .GetByIdAsync(request.ActivityId);

            activity.UpdateIsCancelledStatus(!activity.IsCancelled);

            await this._activitiesDataService.SaveChangesAsync(cancellationToken);
            
            return Result<Unit>.Success(Unit.Value);
        }
    }
}