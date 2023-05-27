using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Spi;
using Schedule.Application.Features.Groups.Commands.Update;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Jobs;

public sealed class TransferGroupsJob : IJob
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public TransferGroupsJob(IScheduleDbContext context,
        IMediator mediator,
        IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }
    
    public async Task Execute(IJobExecutionContext context)
    {
        var groups = await _context.Set<Group>()
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.GroupGroups)
            .Include(e => e.Speciality)
            .Where(e => !e.IsDeleted && e.TermId < e.Speciality.MaxTermId)
            .ToListAsync();

        foreach (var group in groups)
        {
            var transfer = await _context.Set<GroupTransfer>()
                .OrderBy(e => e.NextTermId)
                .FirstOrDefaultAsync(e => e.GroupId == group.GroupId && !e.IsTransferred);
            
            if (transfer is null)
                continue;

            group.TermId = transfer.NextTermId;
            var command = _mapper.Map<UpdateGroupCommand>(group);
            await _mediator.Send(command);
            
            transfer.IsTransferred = true;
            _context.Set<GroupTransfer>().Update(transfer);
        }

        await _context.SaveChangesAsync();
    }
}