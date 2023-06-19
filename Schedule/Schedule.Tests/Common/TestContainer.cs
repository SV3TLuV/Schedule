using Autofac;
using Schedule.Application.Modules;

namespace Schedule.Tests.Common;

internal static class TestContainer
{
    private static readonly IContainer Container;

    static TestContainer()
    {
        var builder = new ContainerBuilder();

        builder.RegisterModule<ApplicationModule>();
        builder.RegisterModule<TestModule>();

        Container = builder.Build();
    }

    public static T Resolve<T>() where T : notnull
    {
        return Container.Resolve<T>();
    }
}