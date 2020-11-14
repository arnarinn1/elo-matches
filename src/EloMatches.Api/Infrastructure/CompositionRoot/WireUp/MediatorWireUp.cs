using EloMatches.Api.Application.Behaviors;
using EloMatches.Api.Infrastructure.CompositionRoot.Implementations;
using EloMatches.Shared.SeedWork;
using FluentValidation;
using MediatR;
using SimpleInjector;

namespace EloMatches.Api.Infrastructure.CompositionRoot.WireUp
{
    public static class MediatorWireUp
    {
        public static Container RegisterMediatorPipeline(this Container container)
        {
            var assembly = typeof(Startup).Assembly;
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), assembly);

            var notificationHandlerTypes = container.GetTypesToRegister(typeof(INotificationHandler<>), new[] { assembly }, new TypesToRegisterOptions
            {
                IncludeGenericTypeDefinitions = true,
                IncludeComposites = false,
            });

            container.Collection.Register(typeof(INotificationHandler<>), notificationHandlerTypes);

            container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            {
                typeof(SendCommandMetricsThroughBusBehavior<,>),
                typeof(LoggingBehavior<,>),
                typeof(ValidatorBehavior<,>),
                typeof(TransactionBehavior<,>),
            });

            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);

            container.Collection.Register(typeof(IValidator<>), typeof(ValidatorBehavior<,>).Assembly);

            container.RegisterSingleton<IResolverFactory, ResolverFactory>();

            return container;
        }
    }
}