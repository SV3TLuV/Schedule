using AutoMapper;
using MediatR;
using Schedule.Application.Features.Timetables.Notifications;
using Schedule.Application.Features.Timetables.Notifications.CreateLessons;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Timetables.Commands.Create;

public sealed class CreateTimetableCommandHandler : IRequestHandler<CreateTimetableCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateTimetableCommandHandler(
        IScheduleDbContext context,
        IMediator mediator,
        IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateTimetableCommand request, CancellationToken cancellationToken)
    {
        var timetable = _mapper.Map<Timetable>(request);
        await _context.Set<Timetable>().AddAsync(timetable, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new CreateLessonsNotification(timetable.TimetableId),
            cancellationToken);
        return timetable.TimetableId;
    }
}