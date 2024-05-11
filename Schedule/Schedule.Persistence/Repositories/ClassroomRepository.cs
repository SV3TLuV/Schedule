using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class ClassroomRepository(IScheduleDbContext context) : IClassroomRepository
{
    public async Task<int> AddIfNotExistAsync(string cabinet, CancellationToken cancellationToken = default)
    {
        int id;

        var classroomDb = await context.Classrooms.FirstOrDefaultAsync(e =>
            e.Cabinet == cabinet, cancellationToken);

        if (classroomDb is null)
        {
            var created = await context.Classrooms.AddAsync(new Classroom
            {
                Cabinet = cabinet
            }, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            id = created.Entity.ClassroomId;
        }
        else if (classroomDb.IsDeleted)
        {
            classroomDb.IsDeleted = false;
            context.Classrooms.Update(classroomDb);

            await context.SaveChangesAsync(cancellationToken);

            id = classroomDb.ClassroomId;
        }
        else
        {
            throw new AlreadyExistsException(cabinet);
        }

        return id;
    }

    public async Task UpdateAsync(Classroom classroom, CancellationToken cancellationToken = default)
    {
        var classroomDb = await context.Classrooms.FirstOrDefaultAsync(e =>
            e.ClassroomId == classroom.ClassroomId, cancellationToken);

        if (classroomDb is null)
        {
            throw new NotFoundException(nameof(Classroom), classroom.ClassroomId);
        }

        var searchByCabinet = await context.Classrooms.FirstOrDefaultAsync(e =>
            e.Cabinet == classroom.Cabinet, cancellationToken);

        if (searchByCabinet is not null)
        {
            throw new AlreadyExistsException(classroom.Cabinet);
        }

        classroomDb.Cabinet = classroom.Cabinet;

        context.Classrooms.Update(classroomDb);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var classroomDb = await context.Classrooms.FirstOrDefaultAsync(e =>
            e.ClassroomId == id, cancellationToken);

        if (classroomDb is null)
        {
            throw new NotFoundException(nameof(Classroom), id);
        }

        classroomDb.IsDeleted = true;

        context.Classrooms.Update(classroomDb);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task RestoreAsync(int id, CancellationToken cancellationToken = default)
    {
        var classroomDb = await context.Classrooms.FirstOrDefaultAsync(e =>
            e.ClassroomId == id, cancellationToken);

        if (classroomDb is null)
        {
            throw new NotFoundException(nameof(Classroom), id);
        }

        classroomDb.IsDeleted = false;

        context.Classrooms.Update(classroomDb);

        await context.SaveChangesAsync(cancellationToken);
    }
}