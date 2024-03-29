﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.Interfaces.DataServices;
using Application.Activities.Models.Output;
using AutoMapper;
using MediatR;
using Reactivities.Common.Result.Models;

namespace Application.Activities.Queries;

public class Categories
{
    public class Query : IRequest<Result<IEnumerable<CategoryOutputModel>>>
    { }

    public class Handler : IRequestHandler<Query, Result<IEnumerable<CategoryOutputModel>>>
    {
        private readonly IActivitiesDataService _activitiesDataService;
        private readonly IMapper _mapper;

        public Handler(IActivitiesDataService activitiesDataService, IMapper mapper)
        {
            this._activitiesDataService = activitiesDataService;
            this._mapper = mapper;
        }

        public async Task<Result<IEnumerable<CategoryOutputModel>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var categories = await this._activitiesDataService
                .GetCategoriesAsync(cancellationToken: cancellationToken);

            var outputModels = this._mapper.Map<IEnumerable<CategoryOutputModel>>(categories);

            return Result<IEnumerable<CategoryOutputModel>>.Success(outputModels);
        }
    }
}