using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Commands.Import;

public sealed class ImportSpecialityCommandHandler : IRequestHandler<ImportSpecialityCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public ImportSpecialityCommandHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
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
            await _context.Set<Speciality>().AddRangeAsync(specialities, cancellationToken);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}