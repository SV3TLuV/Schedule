using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Create;

public sealed class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateGroupCommandHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = _mapper.Map<Group>(request);
        await _context.Set<Group>().AddAsync(group, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return group.GroupId;
    }
}