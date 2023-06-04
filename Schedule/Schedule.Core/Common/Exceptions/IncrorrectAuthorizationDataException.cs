namespace Schedule.Core.Common.Exceptions;

public sealed class IncrorrectAuthorizationDataException : ScheduleException
{
    public IncrorrectAuthorizationDataException()
        : base("Incrorrect login or password.")
    {
    }
}