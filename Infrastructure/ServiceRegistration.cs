using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Settings;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Services;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Repositories
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<ILoggingRepository, LoggingRepository>();
            services.AddTransient<ISchedulesRepository, SchedulesRepository>();

            #endregion

            services.AddTransient<IAuthService, AuthService>();

            services.AddOptions();
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddSingleton<IDatabaseConfig>(_ =>
                new DatabaseConfig(
                    configuration.GetConnectionString("dbConnection")!
                ));
        }
    }
}
