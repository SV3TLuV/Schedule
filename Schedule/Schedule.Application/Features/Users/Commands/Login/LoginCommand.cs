﻿using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Users.Commands.Login;

public sealed class LoginCommand : IRequest<AuthorizationResultViewModel>
{
    public required string Login { get; set; }
    public required string Password { get; set; }
}