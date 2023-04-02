using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Schedule.Api.Common;

namespace Schedule.Api.Controllers;

[ApiController]
[EnableCors(Variables.CorsName)]
[Route("/api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected readonly IMediator Mediator;

    protected BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }
}