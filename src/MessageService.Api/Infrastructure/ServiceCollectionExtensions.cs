using System.Diagnostics.CodeAnalysis;
using MongoDB.Driver;

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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddMongo();

            return services;
        }

        private static IServiceCollection AddMongo(this IServiceCollection services)
        {
            var svcProvider = services.BuildServiceProvider();
            var config = svcProvider.GetRequiredService<IConfiguration>();
            var section = config.GetSection("MongoSettings");
            services.Configure<Mongosettings>(section);
            services.AddScoped<IMongoDBContext, MongoDBContext>();
            return services;
        }
    }
}