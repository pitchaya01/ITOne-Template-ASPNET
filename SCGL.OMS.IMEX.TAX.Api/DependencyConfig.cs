using Autofac;
using Autofac.Core;
using Autofac.Features.Variance;
using FluentValidation;
using Lazarus.Common.CQRS.Implement;
using Lazarus.Common.EventMessaging;
using Lazarus.Common.EventMessaging.EventStore;
using Lazarus.Common.infrastructure;
using Lazarus.Common.Infrastructure.Processing;
using Lazarus.Common.Utilities;
using Lazarus.Common.Validator;
using MediatR;
using MediatR.Pipeline;
using SCGL.OMS.IMEX.TAX.Api.Application.Query;
using SCGL.OMS.IMEX.TAX.Api.Infrastructure;
using SCGL.OMS.IMEX.TAX.Api.Proxy;
using SCGL.OMS.IMEX.TAX.Api.Repository;
using SCGL.OMS.IMEX.TAX.Api.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SCGL.OMS.IMEX.TAX.Api
{
    public class RegisterEventModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<DbDataContext>().InstancePerLifetimeScope();
            builder.RegisterType<DbReadDataContext>().InstancePerLifetimeScope();
            builder.RegisterType<CommandHandlerExecutor>().AsImplementedInterfaces();
            builder.RegisterType<CustomDependencyResolver>().AsImplementedInterfaces();
            builder.RegisterType<EventStore>().AsImplementedInterfaces();


            builder.RegisterType<EventBusKafka>().As<IEventBus>().InstancePerDependency();
            builder.RegisterType<InMemoryEventBusSubscriptionsManager>().As<IEventBusSubscriptionsManager>().InstancePerDependency();
        }
    }

    public class RegisterServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {



            builder.RegisterGeneric(typeof(RepositoryBase<>))
                .AsImplementedInterfaces();
            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.RegisterAssemblyTypes(typeof(DocumentRepository).Assembly)
                 .Where(t => t.Name.EndsWith("Repository"))
                 .AsImplementedInterfaces().InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(DocumentService).Assembly)
                  .Where(t => t.Name.EndsWith("Service"))
                  .AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterAssemblyTypes(typeof(MasterProxy).Assembly)
      .Where(t => t.Name.EndsWith("Proxy"))
      .AsImplementedInterfaces().InstancePerDependency();

        }
    }

    public class SharedModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Lazarus.Common.Authentication.UserLocalService).Assembly)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces().InstancePerDependency();




            builder.RegisterAssemblyTypes(typeof(HelperService).Assembly)
                         .Where(t => t.Name.EndsWith("Service"))
                         .AsImplementedInterfaces().InstancePerDependency();


        }
    }
    public class ProcessingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DomainEventsDispatcher>()
                .As<IDomainEventsDispatcher>()
                .InstancePerLifetimeScope();



            builder.RegisterGenericDecorator(
                typeof(DomainEventsDispatcherCommandHandlerDecorator<,>),
                typeof(IRequestHandler<,>));

            builder.RegisterType<CommandsDispatcher>()
                .As<ICommandsDispatcher>()
                .InstancePerLifetimeScope();


        }
    }
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSource(new ScopedContravariantRegistrationSource(
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
                typeof(IValidator<>)
            ));

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            var mediatrOpenTypes = new[]
            {
            typeof(IRequestHandler<,>),
            //typeof(INotificationHandler<>),
            typeof(IValidator<>),
        };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(typeof(GetDocumentCommand).GetTypeInfo().Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }

            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterGeneric(typeof(CommandValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }


    }
    public class ScopedContravariantRegistrationSource : IRegistrationSource
    {
        private readonly IRegistrationSource _source = new ContravariantRegistrationSource();
        private readonly List<Type> _types = new List<Type>();

        public ScopedContravariantRegistrationSource(params Type[] types)
        {
            if (types == null)
                throw new ArgumentNullException(nameof(types));
            if (!types.All(x => x.IsGenericTypeDefinition))
                throw new ArgumentException("Supplied types should be generic type definitions");
            _types.AddRange(types);
        }


        public IEnumerable<IComponentRegistration> RegistrationsFor(Autofac.Core.Service service, Func<Autofac.Core.Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            var components = _source.RegistrationsFor(service, registrationAccessor);
            foreach (var c in components)
            {
                var defs = c.Target.Services
                    .OfType<TypedService>()
                    .Select(x => x.ServiceType.GetGenericTypeDefinition());

                if (defs.Any(_types.Contains))
                    yield return c;
            }
        }

        public bool IsAdapterForIndividualComponents => _source.IsAdapterForIndividualComponents;
    }
    public class DependencyConfig
    {
    }
}
