using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class LessonChangeRepository(IScheduleDbContext context) : Repository(context), ILessonChangeRepository
{
    public async Task<int> CreateAsync(LessonChange lessonChange, CancellationToken cancellationToken = default)
    {
        var created = await Context.LessonChanges.AddAsync(lessonChange, cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);

        return created.Entity.LessonChangeId;
    }

    public async Task UpdateAsync(LessonChange lessonChange, CancellationToken cancellationToken = default)
    {
        var lessonChangeDb = await Context.LessonChanges.FirstOrDefaultAsync(e =>
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

        Context.LessonChanges.Update(lessonChangeDb);

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var lessonChange = await Context.LessonChanges.FirstOrDefaultAsync(e =>
            e.LessonChangeId == id, cancellationToken);

        if (lessonChange is null)
        {
            throw new NotFoundException(nameof(LessonChange), id);
        }

        Context.LessonChanges.Remove(lessonChange);

        await Context.SaveChangesAsync(cancellationToken);
    }
}