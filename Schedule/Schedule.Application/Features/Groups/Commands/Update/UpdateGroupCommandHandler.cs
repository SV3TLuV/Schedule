using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Update;

public sealed class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateGroupCommandHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var groupDbo = await _context.Set<Group>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.GroupId == request.Id, cancellationToken);

        if (groupDbo is null)
            throw new NotFoundException(nameof(Group), request.Id);

        var searched = await _context.Set<Group>()
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.Speciality)
            .FirstOrDefaultAsync(e =>
                e.Number == request.Number &&
                e.SpecialityId == request.SpecialityId &&
                e.EnrollmentYear == request.EnrollmentYear, cancellationToken);

        if (searched is not null)
            throw new AlreadyExistsException($"Группа: {searched.Name}");
        
        var group = _mapper.Map<Group>(request);

        await _context.Set<GroupGroup>()
            .Where(e =>
                e.GroupId == request.Id ||
                e.GroupId2 == request.Id)
            .AsNoTrackingWithIdentityResolution()
            .ExecuteDeleteAsync(cancellationToken);

        _context.Set<Group>().Update(group);
        await _context.Set<GroupGroup>().AddRangeAsync(group.GroupGroups, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}