using AutoMapper;
using MediatR;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Classrooms.Commands.Create;

public sealed class CreateClassroomCommandHandler : IRequestHandler<CreateClassroomCommand, int>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateClassroomCommandHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateClassroomCommand request, CancellationToken cancellationToken)
    {
        var classroom = _mapper.Map<Classroom>(request);
        await _context.Classrooms.AddAsync(classroom, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return classroom.ClassroomId;
    }
}