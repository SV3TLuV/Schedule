using MediatR;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Context;

namespace Schedule.Application.Jobs;

public sealed class GenerateDatesJob : IJob
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;

    public GenerateDatesJob(IScheduleDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var lastDate = await _context.Set<Date>()
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(e => e.DateId)
            .LastOrDefaultAsync();

        if (lastDate is null)
            throw new NotFoundException(nameof(Date));

        var now = DateTime.UtcNow;
    }
}