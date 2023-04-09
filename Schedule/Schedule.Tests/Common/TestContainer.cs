using Autofac;

namespace Schedule.Tests.Common;

internal static class TestContainer
{
    private static readonly IContainer Container;
    
    static TestContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule<TestModule>();
        Container = builder.Build();
    }

    public static T Resolve<T>() where T : notnull => Container.Resolve<T>();
}