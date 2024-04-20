using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Days.Commands.Update;

public sealed class UpdateDayCommandHandler(
    IScheduleDbContext context,
    IMapper mapper,
    IDayRepository dayRepository) : IRequestHandler<UpdateDayCommand, Unit>
{
    public async Task<Unit> Handle(UpdateDayCommand request, CancellationToken cancellationToken)
    {
        await context.WithTransactionAsync(async () =>
        {
            dayRepository.UseContext(context);
            var day = mapper.Map<Day>(request);
            await dayRepository.UpdateAsync(day, cancellationToken);
        }, cancellationToken);

        return Unit.Value;
    }
}