using AutoMapper;
using MediatR;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Dates.Commands.Create;

public sealed class CreateDateCommandHandler : IRequestHandler<CreateDateCommand, int>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateDateCommandHandler(ScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateDateCommand request, CancellationToken cancellationToken)
    {
        var date = _mapper.Map<Date>(request);
        await _context.Dates.AddAsync(date, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return date.DateId;
    }
}