using MediatR;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Jobs;

public sealed class GenerateDatesJob : IJob
{
    private readonly ScheduleDbContext _context;
    private readonly IMediator _mediator;

    public GenerateDatesJob(ScheduleDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    
    public async Task Execute(IJobExecutionContext context)
    {
        var lastDate = await _context.Dates
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(e => e.DateId)
            .LastOrDefaultAsync();

        if (lastDate is null)
            throw new NotFoundException(nameof(Date));

        var now = DateTime.UtcNow;
        
    }
}

