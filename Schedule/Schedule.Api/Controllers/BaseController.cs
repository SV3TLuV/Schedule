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
    protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
}