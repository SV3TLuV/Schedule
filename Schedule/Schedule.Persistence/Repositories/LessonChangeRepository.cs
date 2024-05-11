using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class LessonChangeRepository(IScheduleDbContext context) : ILessonChangeRepository
{
    public async Task<int> CreateAsync(LessonChange lessonChange, CancellationToken cancellationToken = default)
    {
        return await context.WithTransactionAsync(async () =>
        {
            var lessonChangeDb = await context.LessonChanges.FirstOrDefaultAsync(e =>
                e.LessonId == lessonChange.LessonId &&
                e.Date == lessonChange.Date, cancellationToken);

            if (lessonChangeDb is not null)
            {
                throw new AlreadyExistsException(nameof(LessonChange));
            }

            var created = await context.LessonChanges.AddAsync(lessonChange, cancellationToken);

            foreach (var teacherClassroom in lessonChange.LessonChangeTeacherClassrooms)
            {
                teacherClassroom.LessonChangeId = created.Entity.LessonChangeId;

                await context.LessonChangeTeacherClassrooms.AddAsync(teacherClassroom, cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);

            return created.Entity.LessonChangeId;
        }, cancellationToken);
    }

    public async Task UpdateAsync(LessonChange lessonChange, CancellationToken cancellationToken = default)
    {
        await context.WithTransactionAsync(async () =>
        {
            var lessonChangeDb = await context.LessonChanges.FirstOrDefaultAsync(e =>
                e.LessonChangeId == lessonChange.LessonChangeId, cancellationToken);

            if (lessonChangeDb is null)
            {
                throw new NotFoundException(nameof(LessonChange), lessonChange.LessonChangeId);
            }

            lessonChangeDb.LessonId = lessonChange.LessonId;
            lessonChangeDb.DisciplineId = lessonChange.DisciplineId;
            lessonChangeDb.Subgroup = lessonChange.Subgroup;
            lessonChangeDb.Date = lessonChange.Date;
            lessonChangeDb.TimeStart = lessonChange.TimeStart;
            lessonChangeDb.TimeEnd = lessonChange.TimeEnd;
            lessonChangeDb.Number = lessonChange.Number;

            context.LessonChanges.Update(lessonChangeDb);

            await context.LessonChangeTeacherClassrooms
                .Where(e => e.LessonChangeId == lessonChangeDb.LessonChangeId)
                .ExecuteDeleteAsync(cancellationToken);

            foreach (var teacherClassroom in lessonChange.LessonChangeTeacherClassrooms)
            {
                teacherClassroom.LessonChangeId = lessonChangeDb.LessonChangeId;

                await context.LessonChangeTeacherClassrooms.AddAsync(teacherClassroom, cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var lessonChange = await context.LessonChanges.FirstOrDefaultAsync(e =>
            e.LessonChangeId == id, cancellationToken);

        if (lessonChange is null)
        {
            throw new NotFoundException(nameof(LessonChange), id);
        }

        context.LessonChanges.Remove(lessonChange);

        await context.SaveChangesAsync(cancellationToken);
    }
}