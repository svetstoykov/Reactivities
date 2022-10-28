using System;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Common.Controllers;

public class BuggyController : BaseApiController
{
    public BuggyController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet("not-found")]
    public ActionResult GetNotFound()
    {
        return this.NotFound();
    }

    [HttpGet("bad-request")]
    public ActionResult GetBadRequest()
    {
        return this.BadRequest("This is a bad request");
    }

    [HttpGet("server-error")]
    public ActionResult GetServerError()
    {
        throw new Exception("This is a server error");
    }

    [HttpGet("unauthorised")]
    public ActionResult GetUnauthorised()
    {
        return this.Unauthorized("A diff message");
    }
}