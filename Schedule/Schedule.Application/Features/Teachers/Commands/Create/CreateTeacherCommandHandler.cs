using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Commands.Create;

public sealed class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, int>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public CreateTeacherCommandHandler(IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateTeacherCommand request,
        CancellationToken cancellationToken)
    {
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
        await _context.Set<Teacher>().AddAsync(teacher, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return teacher.TeacherId;
    }
}