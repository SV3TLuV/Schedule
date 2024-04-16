using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Commands.Create;

public sealed class CreateClassroomCommandHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<CreateClassroomCommand, int>
{
    public async Task<int> Handle(CreateClassroomCommand request, CancellationToken cancellationToken)
    {
        var searched = await context.Classrooms
            .AsNoTracking()
            .FirstOrDefaultAsync(e =>
                e.Cabinet == request.Cabinet, cancellationToken);

        if (searched is not null)
            throw new AlreadyExistsException($"Кабинет: {searched.Cabinet}");
        
        var classroom = mapper.Map<Classroom>(request);
        await context.Classrooms.AddAsync(classroom, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return classroom.ClassroomId;
    }
}