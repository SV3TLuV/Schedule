using AutoMapper;
using MediatR;
using Schedule.Application.Features.Dates.Notifications.CreateTimetables;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Dates.Commands.Create;

public sealed class CreateDateCommandHandler : IRequestHandler<CreateDateCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateDateCommandHandler(
        IScheduleDbContext context,
        IMediator mediator,
        IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateDateCommand request, CancellationToken cancellationToken)
    {
        var date = _mapper.Map<Date>(request);
        await _context.Set<Date>().AddAsync(date, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new DateCreateTimetablesNotification(date.DateId), cancellationToken);
        return date.DateId;
    }
}