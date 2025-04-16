using System.Reflection;
using CustomMediatR.Common.Interfaces;

namespace DemoWebAPIWithCustomMediatRAndCQRS
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMediatRHandlers(this IServiceCollection services, Assembly assembly)
        {
            var handlerTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                .ToList();

            foreach (var handlerType in handlerTypes)
            {
                var interfaceTypes = handlerType.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                    .ToList();

                foreach (var interfaceType in interfaceTypes)
                {
                    services.AddTransient(interfaceType, handlerType);
                }
            }

            return services;
        }

        public static IServiceCollection AddPipelineBehaviors(this IServiceCollection services, Assembly assembly)
        {
            var behaviorTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>)))
                .ToList();

            foreach (var behaviorType in behaviorTypes)
            {
                var interfaceTypes = behaviorType.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>))
                    .ToList();

                foreach (var interfaceType in interfaceTypes)
                {
                    services.AddTransient(interfaceType.GetGenericTypeDefinition(), behaviorType.GetGenericTypeDefinition());
                }
            }

            return services;
        }
    }
}
