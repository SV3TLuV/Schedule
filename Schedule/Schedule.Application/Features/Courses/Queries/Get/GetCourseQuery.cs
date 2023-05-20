using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Courses.Queries.Get;

public sealed record GetCourseQuery(int Id) : IRequest<CourseViewModel>;