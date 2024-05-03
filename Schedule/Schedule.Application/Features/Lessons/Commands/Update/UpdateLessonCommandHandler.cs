using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Lessons.Commands.Update;

public sealed class UpdateLessonCommandHandler(
    IScheduleDbContext context,
    ILessonRepository lessonRepository,
    ITimetableRepository timetableRepository,
    IMapper mapper,
    IDateInfoService dateInfoService) : IRequestHandler<UpdateLessonCommand, Unit>
{
    public async Task<Unit> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        await context.WithTransactionAsync(async () =>
        {
            lessonRepository.UseContext(context);
            timetableRepository.UseContext(context);

            var lessonDb = await context.Lessons.FirstOrDefaultAsync(e =>
                e.LessonId == request.LessonId, cancellationToken);

            if (lessonDb is null)
            {
                throw new NotFoundException(nameof(Lesson), request.LessonId);
            }

            var timetable = await context.Timetables.FirstOrDefaultAsync(e =>
                e.TimetableId == lessonDb.TimetableId, cancellationToken);

            if (timetable is null)
            {
                throw new NotFoundException(nameof(Timetable), lessonDb.TimetableId);
            }

            timetable.Ended = dateInfoService.CurrentDate;

            await timetableRepository.UpdateAsync(timetable, cancellationToken);
            var newTimetableId = await timetableRepository.CreateAsync(new Timetable
            {
                GroupId = timetable.GroupId,
                DayId = timetable.DayId,
                WeekTypeId = timetable.WeekTypeId
            }, cancellationToken);

            var lessonsForCopy = (await context.Lessons
                .Include(e => e.LessonTeacherClassrooms)
                .Where(e =>
                    e.TimetableId == timetable.TimetableId &&
                    e.LessonId != request.LessonId)
                .ToListAsync(cancellationToken))
                .Concat(new []
                {
                    mapper.Map<Lesson>(request)
                });

            foreach (var lesson in lessonsForCopy)
            {
                await lessonRepository.CreateAsync(new Lesson
                {
                    DisciplineId = lesson.DisciplineId,
                    Number = lesson.Number,
                    Subgroup = lesson.Subgroup,
                    TimetableId = newTimetableId,
                    TimeStart = lesson.TimeStart,
                    TimeEnd = lesson.TimeEnd,
                    LessonTeacherClassrooms = lesson.LessonTeacherClassrooms
                        .Select(e =>  new LessonTeacherClassroom
                        {
                            TeacherId = e.TeacherId,
                            ClassroomId = e.ClassroomId,
                        }).ToArray()
                }, cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);

        return Unit.Value;
    }
}