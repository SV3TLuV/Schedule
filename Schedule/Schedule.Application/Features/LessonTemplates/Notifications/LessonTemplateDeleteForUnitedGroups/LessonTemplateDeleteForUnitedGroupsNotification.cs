using MediatR;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Notifications.LessonTemplateDeleteForUnitedGroups;

public sealed record LessonTemplateDeleteForUnitedGroupsNotification(LessonTemplate LessonTemplate) : INotification;