﻿using Schedule.Core.Models;

namespace Schedule.Application.Common.Interfaces;

public interface IDateInfoService : IWeekInfoService, IDayInfoService
{
    int CurrentTerm { get; }

    DateTime CurrentDateTime { get; }

    Date CurrentDate { get; }

    Date GetDate(DateTime dateTime);

    Date GetNextDate(DateTime dateTime);

    int GetTerm(DateTime dateTime);

    int GetGroupTerm(int enrollmentYear, bool isAfterEleven);
}