using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonChanges.Commands.Create;

public sealed class CreateLessonChangeCommandHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<CreateLessonChangeCommand, int>
{
    public async Task<int> Handle(CreateLessonChangeCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var lessonChange = mapper.Map<LessonChange>(request);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}