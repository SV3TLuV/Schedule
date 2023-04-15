using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Teachers.Queries.Get;

public sealed record GetTeacherQuery(int Id) : IRequest<TeacherViewModel>;