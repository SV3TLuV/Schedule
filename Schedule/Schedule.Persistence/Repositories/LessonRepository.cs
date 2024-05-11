using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class LessonRepository(IScheduleDbContext context) : ILessonRepository
{
    public async Task<int> CreateAsync(Lesson lesson, CancellationToken cancellationToken = default)
    {
        return await context.WithTransactionAsync(async () =>
        {
            var created = await context.Lessons.AddAsync(lesson, cancellationToken);

            foreach (var teacherClassroom in lesson.LessonTeacherClassrooms)
            {
                teacherClassroom.LessonId = created.Entity.LessonId;

                await context.LessonTeacherClassrooms.AddAsync(teacherClassroom, cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);

            return created.Entity.LessonId;
        }, cancellationToken);
    }

    public async Task UpdateAsync(Lesson lesson, CancellationToken cancellationToken = default)
    {
        await context.WithTransactionAsync(async () =>
        {
            var lessonDb = await context.Lessons.FirstOrDefaultAsync(e =>
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

            context.Lessons.Update(lessonDb);

            await context.LessonTeacherClassrooms
                .Where(e => e.LessonId == lesson.LessonId)
                .ExecuteDeleteAsync(cancellationToken);

            foreach (var teacherClassroom in lesson.LessonTeacherClassrooms)
            {
                teacherClassroom.LessonId = lessonDb.LessonId;

                await context.LessonTeacherClassrooms.AddAsync(teacherClassroom, cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }
}