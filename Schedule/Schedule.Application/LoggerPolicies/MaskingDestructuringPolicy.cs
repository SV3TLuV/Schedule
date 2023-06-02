using Serilog.Core;
using Serilog.Events;

namespace Schedule.Application.LoggerPolicies;

public class MaskingDestructuringPolicy : IDestructuringPolicy
{
    public bool TryDestructure(object value,
        ILogEventPropertyValueFactory propertyValueFactory,
        out LogEventPropertyValue result)
    {
        /*if (value is o request && request.Password != null)
        {
            var properties = new List<LogEventProperty>();
            foreach (var propertyInfo in request.GetType().GetProperties())
            {
                var propertyName = propertyInfo.Name;
                var propertyValue = propertyName.Equals("Password")
                    ? new ScalarValue("****")
                    : propertyValueFactory.CreatePropertyValue(propertyInfo.GetValue(request));
                properties.Add(new LogEventProperty(propertyName, propertyValue));
            }

            result = new StructureValue(properties);
            return true;
        }*/

        result = null!;
        return false;
    }
}