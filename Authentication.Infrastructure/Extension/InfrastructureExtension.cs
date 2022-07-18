using Authentication.Domain.Repositories;
using Authentication.Infra;
using Authentication.Infrastructure.Respositorie;
using System.Text;

namespace Authentication.Infrastructure.Extension
{
    public static class InfrastructureExtension
    {
        public static void AddInfraExtensions(this IServiceCollection services, IConfiguration configuration )
        {
            services.AddMemoryCache();
            services.AddScoped<UserRepository>();
            services.AddScoped<IUserRepository, UserRepositoryInMemory>();
            services.Configure<Settings>(configuration.GetSection("Settings"));           
            services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));           
        }
    }
}


