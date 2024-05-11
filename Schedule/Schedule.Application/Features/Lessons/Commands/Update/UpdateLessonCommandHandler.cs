using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            var lessonDb = await context.Lessons
                .Include(e => e.LessonTeacherClassrooms)
                .Include(e => e.LessonChanges)
                .FirstOrDefaultAsync(e => e.LessonId == request.LessonId, cancellationToken);

            if (lessonDb is null)
            {
                throw new NotFoundException(nameof(Lesson), request.LessonId);
            }

            var mappedLesson = mapper.Map<Lesson>(request);

            var disciplineIsEmpty = lessonDb.DisciplineId is null;
            var subgroupIsEmpty = lessonDb.Subgroup is null;
            var timeIsEmpty = lessonDb.TimeStart is null && lessonDb.TimeEnd is null;
            var changesIsEmpty = lessonDb.LessonChanges.IsNullOrEmpty();
            var teacherAndClassroomsIsEmpty = lessonDb.LessonTeacherClassrooms.IsNullOrEmpty();

            if (disciplineIsEmpty && subgroupIsEmpty && timeIsEmpty && changesIsEmpty && teacherAndClassroomsIsEmpty)
            {
                await lessonRepository.UpdateAsync(mappedLesson, cancellationToken);
                return;
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
                    mappedLesson
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