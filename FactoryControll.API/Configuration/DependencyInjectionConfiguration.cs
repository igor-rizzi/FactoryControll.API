
using FactoryControll.Application.Interfaces.Services;
using FactoryControll.Application.Services;
using FactoryControll.InfraData.Models.Autenticacao;
using FactoryControll.InfraFramework.Dependency;
using System.Reflection;

namespace FactoryControll.API.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.RegisterAutomaticDependencies();

            // Generic services that Scrutor cannot auto-register
            services.AddScoped<IPasswordResetService, PasswordResetService<User>>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        public static void RegisterAutomaticDependencies(this IServiceCollection services)
        {
            services.Scan(selector =>
            {
                selector.FromApplicationDependencies()
                    .AddClasses(typeFilter => typeFilter.AssignableTo<IScopedDependency>())
                    .AsMatchingInterface()
                    .AsImplementedInterfaces()
                    .AsSelf()
                    .WithScopedLifetime();
            });

        }
    }
}
