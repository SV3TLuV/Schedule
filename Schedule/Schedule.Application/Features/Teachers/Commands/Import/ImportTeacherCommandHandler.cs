using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Schedule.Application.Features.Teachers.Commands.Create;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Commands.Import;

public sealed class ImportTeacherCommandHandler : IRequestHandler<ImportTeacherCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ImportTeacherCommandHandler(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(ImportTeacherCommand request, CancellationToken cancellationToken)
    {
        await using var memoryStream = new MemoryStream(request.Content);
        using (var streamReader = new StreamReader(memoryStream))
        {
            var json = await streamReader.ReadToEndAsync(cancellationToken);
            var viewModels = JsonConvert.DeserializeObject<TeacherViewModel[]>(json);
            var teachers = _mapper.Map<Teacher[]>(viewModels);
            var commands = _mapper.Map<CreateTeacherCommand[]>(teachers);

            foreach (var command in commands)
                await _mediator.Send(command, cancellationToken);
        }
        return Unit.Value;
    }
}