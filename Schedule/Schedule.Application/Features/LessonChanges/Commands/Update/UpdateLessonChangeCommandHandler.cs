using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonChanges.Commands.Update;

public sealed class UpdateLessonChangeCommandHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<UpdateLessonChangeCommand, Unit>
{
    public async Task<Unit> Handle(UpdateLessonChangeCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var lessonChange = mapper.Map<LessonChange>(request);

            await transaction.CommitAsync(cancellationToken);
            return Unit.Value;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}