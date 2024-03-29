﻿using System;
using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Enumerations;
using Reactivities.Common.Result.Models;

namespace API.Common.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected readonly IMediator Mediator;
    protected readonly IMapper Mapper;

    public BaseApiController(IMediator mediator, IMapper mapper)
    {
        this.Mediator = mediator;
        this.Mapper = mapper;
    }
        
    protected int GetCurrentUserId => Convert.ToInt32(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
        
    protected string GetCurrentUserUsername => this.User.FindFirstValue(ClaimTypes.Name);

    protected ActionResult HandleMappingResult<TOutputData, TViewModel>(Result<TOutputData> result)
        where TOutputData : class
        where TViewModel : class
    {
        Result<TViewModel> mappedResult = null;
        if (result != null)
        {
            mappedResult = Result<TViewModel>.New(
                this.Mapper.Map<TViewModel>(result.Data),
                result.ResultType,
                result.IsSuccessful,
                result.Message);
        }

        return this.HandleResult(mappedResult);
    }

    protected ActionResult HandleResult<TOutputData>(Result<TOutputData> result)
    {
        var message = result.Message ?? string.Empty;
            
        return result.ResultType switch
        {
            ResultType.Success when result.Data == null => this.NotFound(),
            ResultType.Success => this.Ok(result.Data),
            ResultType.NotFound => this.NotFound(message),
            ResultType.Unauthorized => this.Unauthorized(message),
            _ => this.BadRequest(message)
        };
    }
}