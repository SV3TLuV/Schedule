using AutoMapper;
using MediatR;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Days.Commands.Update;

public sealed class UpdateDayCommandHandler(
    IMapper mapper,
    IDayRepository dayRepository) : IRequestHandler<UpdateDayCommand, Unit>
{
    public async Task<Unit> Handle(UpdateDayCommand request, CancellationToken cancellationToken)
    {
        var day = mapper.Map<Day>(request);
        await dayRepository.UpdateAsync(day, cancellationToken);
        return Unit.Value;
    }
}