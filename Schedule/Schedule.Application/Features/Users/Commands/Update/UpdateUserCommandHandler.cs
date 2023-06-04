using AutoMapper;
using BCrypt.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Users.Commands.Update;

public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userDbo = await _context.Set<User>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.UserId == request.Id, cancellationToken);

        if (userDbo is null)
            throw new NotFoundException(nameof(User), request.Id);

        var user = _mapper.Map<User>(request);
        user.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password, HashType.SHA512);
        _context.Set<User>().Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}