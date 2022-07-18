using Authentication.Domain.Repositories;
using Authentication.Infra;

namespace Authentication.Infrastructure.Extension
{
    public static class InfrastructureExtension
    {
        public static void AddInfraExtensions(this IServiceCollection services, IConfiguration configuration )
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.Configure<Settings>(configuration.GetSection("Settings"));

        }
    }
}
