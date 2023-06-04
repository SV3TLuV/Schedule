namespace Schedule.Core.Common.Exceptions;

public class AuthorizationException : ScheduleException
{
    public AuthorizationException()
        : base("Incrorrect login or password.")
    {
    }
}