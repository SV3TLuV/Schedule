﻿using MediatR;

namespace Schedule.Application.Features.LessonTemplates.Notifications.LessonTemplateUpdateForUnitedGroups;

public sealed record LessonTemplateUpdateForUnitedGroupsNotification(int LessonTemplateId) : INotification;