using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Schedule.Application.Features.Specialities.Commands.Create;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Commands.Import;

public sealed class ImportSpecialityCommandHandler : IRequestHandler<ImportSpecialityCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ImportSpecialityCommandHandler(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(ImportSpecialityCommand request, CancellationToken cancellationToken)
    {
        await using var memoryStream = new MemoryStream(request.Content);
        using (var streamReader = new StreamReader(memoryStream))
        {
            var json = await streamReader.ReadToEndAsync(cancellationToken);
            var viewModels = JsonConvert.DeserializeObject<SpecialityViewModel[]>(json);
            var specialities = _mapper.Map<Speciality[]>(viewModels);
            var commands = _mapper.Map<CreateSpecialityCommand[]>(specialities);

            foreach (var command in commands)
                await _mediator.Send(command, cancellationToken);
        }
        return Unit.Value;
    }
}