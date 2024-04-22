using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using FluentValidation;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Abstractions;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.Common.Mappings;
using Schedule.Application.Services;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Common.Interfaces;
using Schedule.Persistence.Repositories;

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

        builder.RegisterType<MailSenderService>()
            .As<IMailSenderService>();

        RegisterRepositories(builder);

        var services = new ServiceCollection();

        services.AddValidatorsFromAssembly(ThisAssembly);

        builder.Populate(services);
    }

    private void RegisterRepositories(ContainerBuilder builder)
    {
        builder.RegisterType<AccountRepository>()
            .As<IAccountRepository>();

        builder.RegisterType<ClassroomRepository>()
            .As<IClassroomRepository>();

        builder.RegisterType<DayRepository>()
            .As<IDayRepository>();

        builder.RegisterType<DisciplineCodeRepository>()
            .As<IDisciplineCodeRepository>();

        builder.RegisterType<DisciplineNameRepository>()
            .As<IDisciplineNameRepository>();

        builder.RegisterType<DisciplineRepository>()
            .As<IDisciplineRepository>();

        builder.RegisterType<EmployeeRepository>()
            .As<IEmployeeRepository>();

        builder.RegisterType<GroupRepository>()
            .As<IGroupRepository>();

        builder.RegisterType<GroupTransferRepository>()
            .As<IGroupTransferRepository>();

        builder.RegisterType<LessonChangeRepository>()
            .As<ILessonChangeRepository>();

        builder.RegisterType<LessonRepository>()
            .As<ILessonRepository>();

        builder.RegisterType<MiddleNameRepository>()
            .As<IMiddleNameRepository>();

        builder.RegisterType<NameRepository>()
            .As<INameRepository>();

        builder.RegisterType<SessionRepository>()
            .As<ISessionRepository>();

        builder.RegisterType<SpecialityRepository>()
            .As<ISpecialityRepository>();

        builder.RegisterType<StudentRepository>()
            .As<IStudentRepository>();

        builder.RegisterType<SurnameRepository>()
            .As<ISurnameRepository>();

        builder.RegisterType<TeacherRepository>()
            .As<ITeacherRepository>();

        builder.RegisterType<TimetableRepository>()
            .As<ITimetableRepository>();
    }
}