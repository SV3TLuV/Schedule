using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Commands.Import;

public sealed class ImportTeacherCommandHandler : IRequestHandler<ImportTeacherCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public ImportTeacherCommandHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
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
            await _context.Set<Teacher>().AddRangeAsync(teachers, cancellationToken);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}