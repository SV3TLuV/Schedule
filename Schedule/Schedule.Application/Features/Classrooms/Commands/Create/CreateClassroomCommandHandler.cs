using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Commands.Create;

public sealed class CreateClassroomCommandHandler : IRequestHandler<CreateClassroomCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateClassroomCommandHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateClassroomCommand request, CancellationToken cancellationToken)
    {
        var searched = await _context.Set<Classroom>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e =>
                e.Cabinet == request.Cabinet, cancellationToken);

        if (searched is not null)
            throw new AlreadyExistsException($"Кабинет: {searched.Cabinet}");
        
        var classroom = _mapper.Map<Classroom>(request);
        await _context.Set<Classroom>().AddAsync(classroom, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return classroom.ClassroomId;
    }
}