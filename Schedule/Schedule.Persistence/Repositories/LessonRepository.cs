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
        var created = await Context.Lessons.AddAsync(lesson, cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);

        return created.Entity.LessonId;
    }

    public async Task UpdateAsync(Lesson lesson, CancellationToken cancellationToken = default)
    {
        var lessonDb = await Context.Lessons.FirstOrDefaultAsync(e =>
            e.LessonId == lesson.LessonId, cancellationToken);

        if (lessonDb is null)
        {
            throw new NotFoundException(nameof(LessonChange), lesson.LessonId);
        }

        lessonDb.DisciplineId = lesson.DisciplineId;
        lessonDb.Subgroup = lesson.Subgroup;
        lessonDb.TimeStart = lesson.TimeStart;
        lessonDb.TimeEnd = lesson.TimeEnd;
        lessonDb.Number = lesson.Number;

        Context.Lessons.Update(lessonDb);

        await Context.SaveChangesAsync(cancellationToken);
    }
}