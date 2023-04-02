using AutoMapper;
using MediatR;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Times.Commands.Create;

public sealed class CreateTimeCommandHandler : IRequestHandler<CreateTimeCommand, int>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateTimeCommandHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateTimeCommand request, CancellationToken cancellationToken)
    {
        var time = _mapper.Map<Time>(request);
        await _context.Times.AddAsync(time, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return time.TimeId;
    }
}