using AutoMapper;
using MediatR;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.ClassroomTypes.Commands.Create;

public sealed class CreateClassroomTypeCommandHandler : IRequestHandler<CreateClassroomTypeCommand, int>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateClassroomTypeCommandHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateClassroomTypeCommand request, CancellationToken cancellationToken)
    {
        var classroomType = _mapper.Map<ClassroomType>(request);
        await _context.ClassroomTypes.AddAsync(classroomType, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return classroomType.ClassroomTypeId;
    }
}