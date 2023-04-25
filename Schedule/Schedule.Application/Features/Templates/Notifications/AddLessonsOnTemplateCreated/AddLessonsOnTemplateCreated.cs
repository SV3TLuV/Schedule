using MediatR;

namespace Schedule.Application.Features.Templates.Notifications.AddLessonsOnTemplateCreated;

public sealed record AddLessonsOnTemplateCreated(int Id) : INotification;