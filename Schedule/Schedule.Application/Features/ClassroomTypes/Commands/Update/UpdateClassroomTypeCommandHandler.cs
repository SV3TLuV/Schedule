using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.ClassroomTypes.Commands.Update;

public sealed class UpdateClassroomTypeCommandHandler : IRequestHandler<UpdateClassroomTypeCommand>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateClassroomTypeCommandHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateClassroomTypeCommand request, CancellationToken cancellationToken)
    {
        var classroomTypeDbo = await _context.Set<ClassroomType>()
            .FirstOrDefaultAsync(e => e.ClassroomTypeId == request.Id, cancellationToken);

        if (classroomTypeDbo is null)
            throw new NotFoundException(nameof(ClassroomType), request.Id);

        var classroomType = _mapper.Map<ClassroomType>(request);
        _context.Set<ClassroomType>().Update(classroomType);
        await _context.SaveChangesAsync(cancellationToken);
    }
}