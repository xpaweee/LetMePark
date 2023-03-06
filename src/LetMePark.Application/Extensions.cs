using LetMePark.Api.Commands;
using LetMePark.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace LetMePark.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ICommandHandler<>).Assembly;
            services.Scan(s => s.FromAssemblies(applicationAssembly)
                .AddClasses(x => x.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            
            return services;
        }
      
    }
}
