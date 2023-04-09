using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.ClassroomTypes.Commands.Create;

public sealed class CreateClassroomTypeCommandHandler : IRequestHandler<CreateClassroomTypeCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateClassroomTypeCommandHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateClassroomTypeCommand request, CancellationToken cancellationToken)
    {
        var classroomType = _mapper.Map<ClassroomType>(request);
        await _context.Set<ClassroomType>().AddAsync(classroomType, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return classroomType.ClassroomTypeId;
    }
}