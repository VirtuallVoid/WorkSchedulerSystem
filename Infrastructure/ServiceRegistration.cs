using Application.Interfaces;
using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Settings;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

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

            #endregion

            services.AddTransient<IAuthService, AuthService>();

            services.AddOptions();
            services.Configure<JwtSettings>(configuration.GetSection("JWTSettings"));
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
        }
    }
}
