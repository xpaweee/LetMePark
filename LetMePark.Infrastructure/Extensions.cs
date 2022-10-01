using LetMePark.Application.Services;
using LetMePark.Infrastructure.DAL;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using LetMePark.Application.Abstractions;
using LetMePark.Core.Abstractions;
using LetMePark.Infrastructure.Auth;
using LetMePark.Infrastructure.Exceptions;
using LetMePark.Infrastructure.Logging;
using LetMePark.Infrastructure.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

[assembly: InternalsVisibleTo("LetMePark.Tests.Unit")]
namespace LetMePark.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSingleton<ExceptionMiddleware>()
                .AddSingleton<IClock, Clock>()
                .AddPostgres(configuration);
            services.AddSecurity();
            services.AddAuth(configuration);
            services.AddHttpContextAccessor();
            
            var infraAssembly = typeof(AppOptions).Assembly;
            services.Scan(s => s.FromAssemblies(infraAssembly)
                .AddClasses(x => x.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            
            services.AddCustomLogging();
            //swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(swagger =>
            {
                swagger.EnableAnnotations();
                swagger.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "LetMePark API",
                    Version = "v1"
                });
            });
           

            return services;
        }

        public static WebApplication UseInfrastructure(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseSwagger();
            app.UseReDoc(config =>
            {
                config.RoutePrefix = "docs";
                config.SpecUrl("/swagger/v1/swagger.json");
                config.DocumentTitle = "LetMePark API";
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
        
        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var options = new T();
            var section = configuration.GetSection(sectionName);
            section.Bind(options);

            return options;
        }
        
    }
}
