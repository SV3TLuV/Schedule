using AutoMapper;
using MediatR;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.WeekTypes.Commands.Create;

public sealed class CreateWeekTypeCommandHandler : IRequestHandler<CreateWeekTypeCommand, int>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateWeekTypeCommandHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateWeekTypeCommand request, CancellationToken cancellationToken)
    {
        var weekType = _mapper.Map<WeekType>(request);
        await _context.WeekTypes.AddAsync(weekType, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return weekType.WeekTypeId;
    }
}