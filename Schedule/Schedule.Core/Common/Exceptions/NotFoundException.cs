namespace Schedule.Core.Common.Exceptions;

public class NotFoundException : ScheduleException
{
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" with key \"{key}\" was not found.")
    {
    }
}