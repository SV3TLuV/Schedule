namespace Schedule.Application.Common.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class SignalRNotificationAttribute : Attribute
{
    public SignalRNotificationAttribute(
        Type objectType,
        string action)
    {
        ObjectType = objectType;
        Action = action;
    }
    
    public Type ObjectType { get; }
    public string Action { get; }
}