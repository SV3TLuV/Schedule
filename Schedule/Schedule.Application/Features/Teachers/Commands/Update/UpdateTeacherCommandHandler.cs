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

        var searched = await _context.Set<Teacher>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e =>
                e.Name == request.Name &&
                e.Surname == request.Surname &&
                e.MiddleName == request.MiddleName &&
                e.Email == request.Email, cancellationToken);

        if (searched is not null)
            throw new AlreadyExistsException(
                $"Преподаватель: {searched.Surname} {searched.Name} {searched.MiddleName}");
        
        var teacher = _mapper.Map<Teacher>(request);
        _context.Set<Teacher>().Update(teacher);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}