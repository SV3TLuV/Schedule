using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using FluentValidation;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using Microsoft.Extensions.DependencyInjection;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.Common.Mappings;
using Schedule.Application.Services;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Modules;

public sealed class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAutoMapper(options =>
            options.AddProfile(new AssemblyMappingProfile(ThisAssembly)));

        builder.RegisterMediatR(MediatRConfigurationBuilder
            .Create(ThisAssembly)
            .WithAllOpenGenericHandlerTypesRegistered()
            .Build());

        builder.RegisterType<DateInfoService>()
            .As<IDateInfoService>()
            .SingleInstance();

        builder.RegisterType<TokenService>()
            .As<ITokenService>()
            .SingleInstance();

        builder.RegisterType<PasswordHasherService>()
            .As<IPasswordHasherService>()
            .SingleInstance();

        var services = new ServiceCollection();

        services.AddValidatorsFromAssembly(ThisAssembly);

        builder.Populate(services);
    }
}