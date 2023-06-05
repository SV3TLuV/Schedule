using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Commands.Update;

public sealed class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateTeacherCommandHandler(IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateTeacherCommand request,
        CancellationToken cancellationToken)
    {
        var teacherDbo = await _context.Set<Teacher>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TeacherId == request.Id, cancellationToken);

        if (teacherDbo is null)
            throw new NotFoundException(nameof(Teacher), request.Id);

        var teacher = _mapper.Map<Teacher>(request);

        await _context.Set<TeacherGroup>()
            .Where(entity => entity.TeacherId == request.Id)
            .AsNoTrackingWithIdentityResolution()
            .ExecuteDeleteAsync(cancellationToken);

        await _context.Set<TeacherDiscipline>()
            .Where(entity => entity.TeacherId == request.Id)
            .AsNoTrackingWithIdentityResolution()
            .ExecuteDeleteAsync(cancellationToken);

        _context.Set<Teacher>().Update(teacher);
        await _context.Set<TeacherGroup>().AddRangeAsync(teacher.TeacherGroups, cancellationToken);
        await _context.Set<TeacherDiscipline>().AddRangeAsync(teacher.TeacherDisciplines, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}