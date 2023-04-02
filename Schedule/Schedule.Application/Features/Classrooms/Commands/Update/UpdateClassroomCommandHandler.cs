using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Classrooms.Commands.Update;

public sealed class UpdateClassroomCommandHandler : IRequestHandler<UpdateClassroomCommand>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateClassroomCommandHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateClassroomCommand request, CancellationToken cancellationToken)
    {
        var classroomDbo = await _context.Classrooms
            .FirstOrDefaultAsync(e => e.ClassroomId == request.Id, cancellationToken);
        
        if (classroomDbo is null)
            throw new NotFoundException(nameof(Classroom), request.Id);
        
        var classroom = _mapper.Map<Classroom>(request);
        _context.Classrooms.Update(classroom);
        await _context.SaveChangesAsync(cancellationToken);
    }
}