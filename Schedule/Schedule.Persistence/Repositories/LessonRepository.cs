using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class LessonRepository(IScheduleDbContext context) : Repository(context), ILessonRepository
{
    public async Task<int> CreateAsync(Lesson lesson, CancellationToken cancellationToken = default)
    {
        var id = default(int);

        await Context.WithTransactionAsync(async () =>
        {
            var created = await Context.Lessons.AddAsync(lesson, cancellationToken);

            id = created.Entity.LessonId;

            foreach (var teacherClassroom in lesson.LessonTeacherClassrooms)
            {
                teacherClassroom.LessonId = created.Entity.LessonId;

                await Context.LessonTeacherClassrooms.AddAsync(teacherClassroom, cancellationToken);
            }

            await Context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);

        return id;
    }

    public async Task UpdateAsync(Lesson lesson, CancellationToken cancellationToken = default)
    {
        await Context.WithTransactionAsync(async () =>
        {
            var lessonDb = await Context.Lessons.FirstOrDefaultAsync(e =>
                e.LessonId == lesson.LessonId, cancellationToken);

            if (lessonDb is null)
            {
                throw new NotFoundException(nameof(Lesson), lesson.LessonId);
            }

            lessonDb.DisciplineId = lesson.DisciplineId;
            lessonDb.Subgroup = lesson.Subgroup;
            lessonDb.TimeStart = lesson.TimeStart;
            lessonDb.TimeEnd = lesson.TimeEnd;
            lessonDb.Number = lesson.Number;

            Context.Lessons.Update(lessonDb);

            await Context.LessonTeacherClassrooms
                .Where(e => e.LessonId == lesson.LessonId)
                .ExecuteDeleteAsync(cancellationToken);

            foreach (var teacherClassroom in lesson.LessonTeacherClassrooms)
            {
                teacherClassroom.LessonId = lesson.LessonId;

                await Context.LessonTeacherClassrooms.AddAsync(teacherClassroom, cancellationToken);
            }

            await Context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }
}