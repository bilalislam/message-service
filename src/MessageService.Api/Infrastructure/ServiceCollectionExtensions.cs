using System.Diagnostics.CodeAnalysis;

namespace MessageService.Api
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddProxies();
            services.AddRepositories();
            return services;
        }

        private static IServiceCollection AddProxies(this IServiceCollection services)
        {
            services.AddScoped<ITokenProxy, TokenProxy>();
            return services;
        }
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IBlockedUserRepository, BlockedUserRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddSingleton<IMongoDBContext, MongoDBContext>();
            return services;
        }
    }
}