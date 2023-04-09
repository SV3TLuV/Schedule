using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Times.Commands.Create;

public sealed class CreateTimeCommandHandler : IRequestHandler<CreateTimeCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateTimeCommandHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateTimeCommand request, CancellationToken cancellationToken)
    {
        var time = _mapper.Map<Time>(request);
        await _context.Set<Time>().AddAsync(time, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return time.TimeId;
    }
}