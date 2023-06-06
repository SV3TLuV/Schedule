using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Schedule.Api.Common;
using Schedule.Application.ViewModels;

namespace Schedule.Api.Controllers;

[ApiController]
[EnableCors(Constants.CorsName)]
[Route("/api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
}