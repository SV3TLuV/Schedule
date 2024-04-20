using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class ClassroomRepository(IScheduleDbContext context) : Repository(context), IClassroomRepository
{
    public async Task AddIfNotExists(string cabinet, CancellationToken cancellationToken = default)
    {
        var classroomDb = await Context.Classrooms.FirstOrDefaultAsync(e =>
            e.Cabinet == cabinet, cancellationToken);

        if (classroomDb is null)
        {
            await Context.Classrooms.AddAsync(new Classroom
            {
                Cabinet = cabinet
            }, cancellationToken);
        }
        else if (classroomDb.IsDeleted)
        {
            classroomDb.IsDeleted = false;
            Context.Classrooms.Update(classroomDb);
        }
        else
        {
            throw new AlreadyExistsException(cabinet);
        }

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Classroom classroom, CancellationToken cancellationToken = default)
    {
        var classroomDb = await Context.Classrooms.FirstOrDefaultAsync(e =>
            e.ClassroomId == classroom.ClassroomId, cancellationToken);

        if (classroomDb is null)
        {
            throw new NotFoundException(nameof(Classroom), classroom.ClassroomId);
        }

        var searchByCabinet = await Context.Classrooms.FirstOrDefaultAsync(e =>
            e.Cabinet == classroom.Cabinet, cancellationToken);

        if (searchByCabinet is not null)
        {
            throw new AlreadyExistsException(classroom.Cabinet);
        }

        classroomDb.Cabinet = classroom.Cabinet;

        Context.Classrooms.Update(classroomDb);

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var classroomDb = await Context.Classrooms.FirstOrDefaultAsync(e =>
            e.ClassroomId == id, cancellationToken);

        if (classroomDb is null)
        {
            throw new NotFoundException(nameof(Classroom), id);
        }

        classroomDb.IsDeleted = true;

        Context.Classrooms.Update(classroomDb);

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task RestoreAsync(int id, CancellationToken cancellationToken = default)
    {
        var classroomDb = await Context.Classrooms.FirstOrDefaultAsync(e =>
            e.ClassroomId == id, cancellationToken);

        if (classroomDb is null)
        {
            throw new NotFoundException(nameof(Classroom), id);
        }

        classroomDb.IsDeleted = false;

        Context.Classrooms.Update(classroomDb);

        await Context.SaveChangesAsync(cancellationToken);
    }
}