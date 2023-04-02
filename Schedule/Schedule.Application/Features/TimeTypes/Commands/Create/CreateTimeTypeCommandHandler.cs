using AutoMapper;
using MediatR;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.TimeTypes.Commands.Create;

public sealed class CreateTimeTypeCommandHandler : IRequestHandler<CreateTimeTypeCommand, int>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateTimeTypeCommandHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateTimeTypeCommand request, CancellationToken cancellationToken)
    {
        var timeType = _mapper.Map<TimeType>(request);
        await _context.TimeTypes.AddAsync(timeType, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return timeType.TimeTypeId;
    }
}