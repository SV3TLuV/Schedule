﻿using MediatR;

namespace Schedule.Application.Features.LessonTemplates.Notifications.LessonTemplateCreateForUnitedGroups;

public sealed record LessonTemplateCreateForUnitedGroupsNotification(int LessonTemplateId) : INotification;