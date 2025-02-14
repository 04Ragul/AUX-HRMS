using Microsoft.Extensions.DependencyInjection;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Application.Interfaces.Services.Storage;
using HRMS.Application.Interfaces.Services.Storage.Provider;
using HRMS.Infrastructure.Repositories;
using HRMS.Infrastructure.Services.Storage;
using HRMS.Infrastructure.Services.Storage.Provider;
using System.Reflection;
using HRMS.Shared.Utilities.Serialization.Options;
using HRMS.Shared.Utilities.Interfaces.Serialization.Serializers;
using HRMS.Shared.Utilities.Serialization.Serializers;
using HRMS.Shared.Utilities.Serialization.JsonConverters;

namespace HRMS.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void InfrastructureMappings(this IServiceCollection services)
        {
            _ = services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
            .AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
             .AddTransient(typeof(IDapperRepository),typeof(DapperRepository))
                .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }


        public static IServiceCollection AddServerStorage(this IServiceCollection services)
        {
            return AddServerStorage(services, null!);
        }

        public static IServiceCollection AddServerStorage(this IServiceCollection services, Action<SystemTextJsonOptions> configure)
        {
            return services
                .AddScoped<IJsonSerializer, SystemTextJsonSerializer>()
                .AddScoped<IStorageProvider, ServerStorageProvider>()
                .AddScoped<IServerStorageService, ServerStorageService>()
                .AddScoped<ISyncServerStorageService, ServerStorageService>()
                .Configure<SystemTextJsonOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    if (!configureOptions.JsonSerializerOptions.Converters.Any(c => c.GetType() == typeof(TimespanJsonConverter)))
                    {
                        configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                    }
                });
        }
    }
}
